using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Utils
{
    static class Mapper 
    {
        public static (ContaSaldo, ContaSaldo) map(RegistroTransacional registroTransacional)
        {
            var contaOrigem = new ContaSaldo(registroTransacional.ContaOrigem, registroTransacional.ValorFinalContaOrigem);
            var contaDestino = new ContaSaldo(registroTransacional.ContaDestino, registroTransacional.ValorFinalContaDestino);
            return (contaOrigem, contaDestino);
        }
    }
}