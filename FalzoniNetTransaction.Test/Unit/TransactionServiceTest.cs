using FalzoniNetTransaction.Model;
using FalzoniNetTransaction.Service;

namespace FalzoniNetTransaction.Test.Unit
{
    public class TransactionServiceTest
    {
        private readonly TransactionService _service;
        public TransactionServiceTest()
        {
            _service = new TransactionService();
        }

        [Fact]
        public void TransactionService_Receive_Success_Test()
        {
            // Arrange
            Transaction transaction = new Transaction
            {
                Valor = 1000,
                DataHora = DateTime.Now
            };
            // Act
            _service.Receive(transaction);


            // Assert
            Assert.True(_service.Transactions.Count() > 0);
            Assert.False(!_service.Transactions.Any());
        }

        [Fact]
        public void TransactionService_Receive_Throws_Nullable_Test()
        {
            // Arrange, Act and Assert
            Assert.Throws<NullReferenceException>(() => _service.Receive(null));
        }

        [Fact]
        public void TransactionService_GetStatistics_Success_Test()
        {

            // Arrange
            _service.Clear();

            Transaction transaction1 = new Transaction
            {
                Valor = 1000,
                DataHora = DateTime.Now.AddSeconds(-10)
            };

            Transaction transaction2 = new Transaction
            {
                Valor = 2000,
                DataHora = DateTime.Now.AddSeconds(-20)
            };

            Transaction transaction3 = new Transaction
            {
                Valor = 1000,
                DataHora = DateTime.Now.AddSeconds(-30)
            };
            _service.Receive(transaction1);
            _service.Receive(transaction2);
            _service.Receive(transaction3);

            // Act
            SummaryStatistics statistics = _service.GetStatistics();

            // Assert
            Assert.Equal(3, statistics.Count);
            Assert.Equal(4000, statistics.Sum);
            Assert.Equal(1333.33M, Math.Round(statistics.Average, 2));
            Assert.Equal(1000, statistics.Min);
            Assert.Equal(2000, statistics.Max);
        }
    }
}
