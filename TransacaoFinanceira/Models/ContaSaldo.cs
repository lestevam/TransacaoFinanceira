using System;

namespace TransacaoFinanceira.Models
{
    public class ContaSaldo
    {
        public long conta { get; set; }
        public decimal saldo { get; set; }

        public ContaSaldo(long conta, decimal valor)
        {
            this.conta = conta;
            this.saldo = valor;
        }
    }
}