using System;
using System.Collections.Generic;
using TransacaoFinanceira.Models;
using TransacaoFinanceira.Repositories;
using TransacaoFinanceira.Services;
using Xunit;

namespace TransacaoFinanceiraTests.Tests
{
    public class TransacaoFinanceiraRepositoryTests
    {
        private readonly ContasRepository _repository;

        public TransacaoFinanceiraRepositoryTests()
        {
            _repository = new ContasRepository();
        }

        [Fact]
        public void Existe_DeveRetornarTrue_QuandoContaExistente()
        {
            // Arrange
            long contaExistente = 938485762;

            // Act
            bool resultado = _repository.existe(contaExistente);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void Existe_DeveRetornarFalse_QuandoContaNaoExistente()
        {
            // Arrange
            long contaNaoExistente = 999999999;

            // Act
            bool resultado = _repository.existe(contaNaoExistente);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public void GetSaldo_DeveRetornarSaldoCorreto_QuandoContaExistente()
        {
            // Arrange
            long contaExistente = 347586970;

            // Act
            ContaSaldo saldo = _repository.getSaldo(contaExistente);

            // Assert
            Assert.NotNull(saldo);
            Assert.Equal(1200, saldo.saldo);
        }

        [Fact]
        public void GetSaldo_DeveRetornarNull_QuandoContaNaoExistente()
        {
            // Arrange
            long contaNaoExistente = 999999999;

            // Act
            ContaSaldo saldo = _repository.getSaldo(contaNaoExistente);

            // Assert
            Assert.Null(saldo);
        }

        [Fact]
        public void Atualizar_DeveRetornarTrue_QuandoAtualizacaoBemSucedida()
        {
            // Arrange
            var contaSaldo = new ContaSaldo(938485762, 500);

            // Act
            bool resultado = _repository.atualizar(contaSaldo);

            // Assert
            Assert.True(resultado);
            var saldoAtualizado = _repository.getSaldo(938485762);
            Assert.Equal(500, saldoAtualizado.saldo);
        }

    }
}