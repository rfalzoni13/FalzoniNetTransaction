using FalzoniNetTransaction.Model;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FalzoniNetTransaction.Service
{
    public class TransactionService
    {
        private List<Transaction> _transactions;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService()
        {
            _transactions = _transactions ?? new List<Transaction>();
            _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<TransactionService>();
        }

        public IEnumerable<Transaction> Transactions => _transactions;

        public void Clear()
        {
            _logger.LogInformation("Limpando transações registradas");
            _transactions.Clear();
        }

        public void Receive(Transaction? transaction)
        {
            _logger.LogInformation("Validando transação");

            if (transaction == null) throw new NullReferenceException("Objeto nulo ou inválido");

            _logger.LogInformation("Transação validada - adicionando ao histórico");

            _transactions.Add(transaction!);
            
        }

        public SummaryStatistics GetStatistics()
        {
            _logger.LogInformation("Calculando estatísticas das transações");
            
            Stopwatch stopwatch = Stopwatch.StartNew();

            stopwatch.Start();

            List<Transaction> validTransactions = _transactions
                .Where(x => 
                (x.DataHora.HasValue && x.DataHora.Value >= DateTime.Now.AddSeconds(-60)) &&
                (x.DataHora.HasValue && x.DataHora.Value < DateTime.Now))
                .ToList();

            SummaryStatistics statistics = new SummaryStatistics(
                validTransactions.Count(),
                validTransactions.Sum(x => x.Valor) ?? 0,
                validTransactions.Average(x => x.Valor) ?? 0,
                validTransactions.Min(x => x.Valor) ?? 0,
                validTransactions.Max(x => x.Valor) ?? 0);

            stopwatch.Stop();

            _logger.LogInformation($"Estatísticas calculadas em {stopwatch.ElapsedMilliseconds}ms");

            return statistics;
        }
    }
}
