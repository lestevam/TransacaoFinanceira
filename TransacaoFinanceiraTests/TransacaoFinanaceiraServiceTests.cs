using Moq;
using System.Collections.Concurrent;
using TransacaoFinanceira.Interfaces;
using TransacaoFinanceira.Models;
using TransacaoFinanceira.Services;

namespace TransacaoFinanceiraTests.Tests
{
    public class TransacaoFinanceiraServiceTests
    {
        private readonly Mock<IContasRepository> _repositoryMock;
        private readonly Mock<ITransacao> _transacaoMock;
        private readonly TransacaoFinanceiraService _service;

        public TransacaoFinanceiraServiceTests()
        {
            _repositoryMock = new Mock<IContasRepository>();
            _transacaoMock = new Mock<ITransacao>();
            _service = new TransacaoFinanceiraService(_repositoryMock.Object, _transacaoMock.Object);
        }

        [Fact]
        public void Processar_DeveProcessarTransacoes()
        {
            // Arrange
            RegistroTransacional[] transacoes = new RegistroTransacional[]
            {
                new RegistroTransacional {CorrelationId=1, DtTransacao="09/09/2023 14:15:00", ContaOrigem=938485762, ContaDestino=2147483649, Valor=150},
                new RegistroTransacional {CorrelationId=2, DtTransacao="09/09/2023 14:15:05", ContaOrigem=2147483649, ContaDestino=210385733, Valor=149}
            };

            _repositoryMock.Setup(r => r.existe(It.IsAny<long>())).Returns(true);
            _repositoryMock.Setup(r => r.getSaldo(It.IsAny<long>())).Returns(new ContaSaldo(5978532, 1000m));
            _transacaoMock.Setup(t => t.validarTransferencia(It.IsAny<RegistroTransacional>())).Returns(true);
            _transacaoMock.Setup(t => t.transferir(It.IsAny<RegistroTransacional>())).Returns((RegistroTransacional rt) => rt);
            
            // Act
            _service.processar(transacoes);
            
            // Assert
            _repositoryMock.Verify(r => r.atualizar(It.IsAny<ContaSaldo>()), Times.Exactly(4));
        }

        [Fact]
        public void ProcessarParallel_DeveProcessarTransacoesEmParalelo()
        {
            // Arrange
            RegistroTransacional[] transacoes = new RegistroTransacional[]
            {
                new RegistroTransacional {CorrelationId=1, DtTransacao="09/09/2023 14:15:00", ContaOrigem=938485762, ContaDestino=2147483649, Valor=150},
                new RegistroTransacional {CorrelationId=2, DtTransacao="09/09/2023 14:15:05", ContaOrigem=2147483649, ContaDestino=210385733, Valor=149}
            };

            _repositoryMock.Setup(r => r.existe(It.IsAny<long>())).Returns(true);
            _repositoryMock.Setup(r => r.getSaldo(It.IsAny<long>())).Returns(new ContaSaldo(5978532, 1000m));
            _transacaoMock.Setup(t => t.validarTransferencia(It.IsAny<RegistroTransacional>())).Returns(true);
            _transacaoMock.Setup(t => t.transferir(It.IsAny<RegistroTransacional>())).Returns((RegistroTransacional rt) => rt);
            
            var transacoesOrdenadas = transacoes.OrderBy(t => DateTime.Parse(t.DtTransacao)).ToList();
            var queue = new ConcurrentQueue<RegistroTransacional>(transacoesOrdenadas);

            // Act
            _service.processarParallel(queue);

            // Assert
            _repositoryMock.Verify(r => r.atualizar(It.IsAny<ContaSaldo>()), Times.Exactly(4));
        }
    }
}
