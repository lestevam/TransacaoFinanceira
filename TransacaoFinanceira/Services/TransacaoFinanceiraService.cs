using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using TransacaoFinanceira.Interfaces;
using TransacaoFinanceira.Models;
using TransacaoFinanceira.Utils;

namespace TransacaoFinanceira.Services
{
    public class TransacaoFinanceiraService
	{
		private IContasRepository _repository;
		private ITransacao _transacao;
		private static readonly object _lock = new object();

		public TransacaoFinanceiraService(IContasRepository repository, ITransacao transacao)
		{
			_repository = repository;
			_transacao = transacao;
		}

		public void processar(RegistroTransacional[] transacoes)
		{
			foreach (var item in transacoes)
			{
				processarTransferencia(item);
			};
		}

		public void processarParallel(ConcurrentQueue<RegistroTransacional> transacoes)
		{
			Parallel.ForEach(transacoes, item =>
			{
				lock (_lock)
				{ 
					processarTransferencia(item);
				}
			});
		}

		private void processarTransferencia(RegistroTransacional registroTransacional)
		{
			registroTransacional.ContaOrigemExistente = _repository.existe(registroTransacional.ContaOrigem);
			registroTransacional.ContaDestinoExistente = _repository.existe(registroTransacional.ContaDestino);
			registroTransacional.ValorAtualContaOrigem = _repository.getSaldo(registroTransacional.ContaOrigem)?.saldo ?? 0;
			registroTransacional.ValorAtualContaDestino = _repository.getSaldo(registroTransacional.ContaDestino)?.saldo ?? 0;
			try
			{
				if (!_transacao.validarTransferencia(registroTransacional))
				{
					Console.WriteLine($"Transacao numero {registroTransacional.CorrelationId} foi cancelada por falta de saldo");
				}
				else
				{
					var registroTransacionalCalculado = _transacao.transferir(registroTransacional);
					(var contaOrigemCalculada, var contaDestinoCalculada) = Mapper.map(registroTransacional);
					_repository.atualizar(contaOrigemCalculada);
					_repository.atualizar(contaDestinoCalculada);
					Console.WriteLine($"Transacao numero {registroTransacional.CorrelationId} foi efetivada com sucesso! Novos saldos: Conta Origem:{contaOrigemCalculada.saldo} | Conta Destino: {contaDestinoCalculada.saldo}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Impossível realizar transação para registro numero {registroTransacional.CorrelationId}. Motivo: {ex.Message}");
			}
		}

	}
}