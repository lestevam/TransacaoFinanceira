using System;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Interfaces
{
    public interface ITransacao
    {
        public bool validarTransferencia(RegistroTransacional registroTransacional);
        public RegistroTransacional transferir(RegistroTransacional registroTransacional);
    }
}