using System;
using System.Collections.Concurrent;
using System.Linq;
using TransacaoFinanceira.Domain;
using TransacaoFinanceira.Models;
using TransacaoFinanceira.Repositories;
using TransacaoFinanceira.Services;

namespace TransacaoFinanceira
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistroTransacional[] transacoes = new RegistroTransacional[] { 
                                     new RegistroTransacional {CorrelationId=1, DtTransacao="09/09/2023 14:15:00", ContaOrigem=938485762, ContaDestino=2147483649, Valor=150},
                                     new RegistroTransacional {CorrelationId=2, DtTransacao="09/09/2023 14:15:05", ContaOrigem=2147483649, ContaDestino=210385733, Valor=149},
                                     new RegistroTransacional {CorrelationId=3, DtTransacao="09/09/2023 14:15:29", ContaOrigem=347586970, ContaDestino=238596054, Valor=1100},
                                     new RegistroTransacional {CorrelationId=4, DtTransacao="09/09/2023 14:17:00", ContaOrigem=675869708, ContaDestino=210385733, Valor=5300},
                                     new RegistroTransacional {CorrelationId=5, DtTransacao="09/09/2023 14:18:00", ContaOrigem=238596054, ContaDestino=674038564, Valor=1489},
                                     new RegistroTransacional {CorrelationId=6, DtTransacao="09/09/2023 14:18:20", ContaOrigem=573659065, ContaDestino=563856300, Valor=49},
                                     new RegistroTransacional {CorrelationId=7, DtTransacao="09/09/2023 14:19:00", ContaOrigem=938485762, ContaDestino=2147483649, Valor=44},
                                     new RegistroTransacional {CorrelationId=8, DtTransacao="09/09/2023 14:19:01", ContaOrigem=573659065, ContaDestino=675869708, Valor=150},
                                     new RegistroTransacional {CorrelationId=9, DtTransacao="09/09/2023 14:19:02", ContaOrigem=573659065999, ContaDestino=675869708, Valor=150},

            };
            var executor = new TransacaoFinanceiraService(new ContasRepository(), new Transacao());
            executor.processar(transacoes);
            Console.WriteLine("Processamento finalizado");
            Console.WriteLine(new string('-', 120));

            var executorParallel = new TransacaoFinanceiraService(new ContasRepository(), new Transacao());
            var transacoesOrdenadas = transacoes.OrderBy(t => DateTime.Parse(t.DtTransacao)).ToList();
            var queue = new ConcurrentQueue<RegistroTransacional>(transacoesOrdenadas);
            executorParallel.processarParallel(queue);
            Console.WriteLine("Processamento finalizado");
            Console.WriteLine(new string('-', 120));

            Console.ReadLine();
        }
    }

    
    
   
}
