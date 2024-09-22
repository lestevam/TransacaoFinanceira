using System;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Interfaces
{
    public interface IContasRepository
    {
        public bool existe(long conta);
        public ContaSaldo getSaldo(long conta);
        public bool atualizar(ContaSaldo conta);
    }
}