using TransacaoFinanceira.Domain;
using TransacaoFinanceira.Exceptions;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceiraTests.Tests
{
    public class TransacaoFinanceiraDomainTests
    {
        private readonly Transacao _transacao;

        public TransacaoFinanceiraDomainTests()
        {
            _transacao = new Transacao();
        }

        [Fact]
        public void ValidarTransferencia_DeveRetornarTrue_QuandoSaldoSuficiente()
        {
            // Arrange
            var registro = new RegistroTransacional
            {
                ContaOrigemExistente = true,
                ContaDestinoExistente = true,
                ValorAtualContaOrigem = 1000m,
                Valor = 500m
            };

            // Act
            var resultado = _transacao.validarTransferencia(registro);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarTransferencia_DeveLancarExcecao_QuandoContaOrigemNaoExistente()
        {
            // Arrange
            var registro = new RegistroTransacional
            {
                ContaOrigemExistente = false,
                ContaDestinoExistente = true,
                ValorAtualContaOrigem = 1000m,
                Valor = 500m
            };

            // Act & Assert
            var ex = Assert.Throws<TransferenciaInvalidaException>(() => _transacao.validarTransferencia(registro));
            Assert.Equal("Conta não reconhecida", ex.Message);
        }

        [Fact]
        public void ValidarTransferencia_DeveLancarExcecao_QuandoContaDestinoNaoExistente()
        {
            // Arrange
            var registro = new RegistroTransacional
            {
                ContaOrigemExistente = true,
                ContaDestinoExistente = false,
                ValorAtualContaOrigem = 1000m,
                Valor = 500m
            };

            // Act & Assert
            var ex = Assert.Throws<TransferenciaInvalidaException>(() => _transacao.validarTransferencia(registro));
            Assert.Equal("Conta não reconhecida", ex.Message);
        }

        [Fact]
        public void Transferir_DeveAtualizarValores_QuandoTransferenciaValida()
        {
            // Arrange
            var registro = new RegistroTransacional
            {
                ContaOrigemExistente = true,
                ContaDestinoExistente = true,
                ValorAtualContaOrigem = 1000m,
                ValorAtualContaDestino = 500m,
                Valor = 200m
            };

            // Act
            var resultado = _transacao.transferir(registro);

            // Assert
            Assert.Equal(800m, resultado.ValorFinalContaOrigem);
            Assert.Equal(700m, resultado.ValorFinalContaDestino);
        }

        [Fact]
        public void Transferir_DeveLancarExcecao_QuandoSaldoInsuficiente()
        {
            // Arrange
            var registro = new RegistroTransacional
            {
                ContaOrigemExistente = true,
                ContaDestinoExistente = true,
                ValorAtualContaOrigem = 100m,
                Valor = 200m
            };

            // Act & Assert
            var ex = Assert.Throws<TransferenciaInvalidaException>(() => _transacao.transferir(registro));
            Assert.Equal("Saldo em conta inválido", ex.Message);
        }
    }
}