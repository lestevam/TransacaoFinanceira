using System;

namespace TransacaoFinanceira.Models
{
    public class RegistroTransacional
    {
        public int CorrelationId { get; set; }
        public string DtTransacao { get; set; }
        public long ContaOrigem { get; set; }
        public long ContaDestino { get; set; }
        public bool ContaOrigemExistente { get; set; }
        public bool ContaDestinoExistente { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorAtualContaOrigem { get; set; }
        public decimal ValorAtualContaDestino { get; set; }
        public decimal ValorFinalContaOrigem { get; set; }
        public decimal ValorFinalContaDestino { get; set; }
    }
}