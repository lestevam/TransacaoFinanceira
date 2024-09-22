using System;
using System.Collections.Generic;
using TransacaoFinanceira.Interfaces;
using TransacaoFinanceira.Models;

namespace TransacaoFinanceira.Repositories
{
    public class ContasRepository : IContasRepository
    {

        #region Properties
        private List<ContaSaldo> TabelaSaldos { get; set; }
        #endregion

        #region Constructor
        public ContasRepository()
        {
            TabelaSaldos = new List<ContaSaldo>();
            TabelaSaldos.Add(new ContaSaldo(938485762, 180));
            TabelaSaldos.Add(new ContaSaldo(347586970, 1200));
            TabelaSaldos.Add(new ContaSaldo(2147483649, 0));
            TabelaSaldos.Add(new ContaSaldo(675869708, 4900));
            TabelaSaldos.Add(new ContaSaldo(238596054, 478));
            TabelaSaldos.Add(new ContaSaldo(573659065, 787));
            TabelaSaldos.Add(new ContaSaldo(210385733, 10));
            TabelaSaldos.Add(new ContaSaldo(674038564, 400));
            TabelaSaldos.Add(new ContaSaldo(563856300, 1200));
        }
        #endregion

        #region Methods
        public bool existe(long conta)
        {
            return TabelaSaldos.Exists(s => s.conta == conta);
        }

        public ContaSaldo getSaldo(long conta)
        {
            return TabelaSaldos.Find(registro => registro.conta == conta);
        }

        public bool atualizar(ContaSaldo contaSaldo)
        {
            try
            {
                TabelaSaldos.RemoveAll(x => x.conta == contaSaldo.conta);
                TabelaSaldos.Add(contaSaldo);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion
    }
}