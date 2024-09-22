using System;
using TransacaoFinanceira.Models;
using TransacaoFinanceira.Interfaces;
using TransacaoFinanceira.Exceptions;

namespace TransacaoFinanceira.Domain
{
    public class Transacao : ITransacao
    {
        public bool validarTransferencia(RegistroTransacional registroTransacional)
        {
            if (registroTransacional is {ContaOrigemExistente: false } || registroTransacional is {ContaDestinoExistente: false }) throw new TransferenciaInvalidaException("Conta não reconhecida");
            if (registroTransacional.ValorAtualContaOrigem > registroTransacional.Valor) return true;
            return false;
        }

        public RegistroTransacional transferir(RegistroTransacional registroTransacional)
        {
            if (validarTransferencia(registroTransacional))
            {
                registroTransacional.ValorFinalContaOrigem = registroTransacional.ValorAtualContaOrigem - registroTransacional.Valor;
                registroTransacional.ValorFinalContaDestino = registroTransacional.ValorAtualContaDestino + registroTransacional.Valor;
                return registroTransacional;
            }
            else
            {
                throw new TransferenciaInvalidaException("Saldo em conta inválido");
            }
        }
    }
}