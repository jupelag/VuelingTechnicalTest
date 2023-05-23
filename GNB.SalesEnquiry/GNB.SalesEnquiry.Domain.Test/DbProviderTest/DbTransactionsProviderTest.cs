using FluentAssertions;
using GNB.SalesEnquiry.DbProvider;
using GNB.SalesEnquiry.DbProvider.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;
using Moq;
using Xunit;

namespace GNB.SalesEnquiry.Domain.Test.DbProviderTest
{
    public class DbTransactionsProviderTest
    {
        private ITransactionsDao GetTransactionsDaoMockObjectToGetAllTransactions(TransactionDto[] expectedResponse)
        {
            var mock = new Mock<ITransactionsDao>();
            mock.Setup(m => m.GetTransactionAsync()).Returns(Task.FromResult(expectedResponse));
            return mock.Object;
        }
        private ITransactionsDao GetTransactionsDaoMockObjectToGetTransaction(TransactionDto[] expectedResponse)
        {
            var mock = new Mock<ITransactionsDao>();
            mock.Setup(m => m.GetTransactionsBySku(It.IsAny<string>())).Returns(Task.FromResult<TransactionDto[]>(expectedResponse));
            return mock.Object;
        }

        [Fact]
        public async Task GetTransactionAsync()
        {
            var transactions = TransactionsTestData.GetTransactions();
            var transactionsDaoMock = GetTransactionsDaoMockObjectToGetAllTransactions(transactions);
            var provider = new DbTransactionsProvider(transactionsDaoMock);

            var result = await provider.GetTransactionsAsync();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(transactions);
        }

        [Fact]
        public async Task GetTransactionAsync_when_dao_throws_exception()
        {
            var transactionsDaoMock = new Mock<ITransactionsDao>();
            transactionsDaoMock.Setup(m => m.GetTransactionAsync()).Throws<Exception>();
            var provider = new DbTransactionsProvider(transactionsDaoMock.Object);
            await FluentActions.Awaiting(() => provider.GetTransactionsAsync()).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetTransactionBySkyAsync()
        {
            var transactions = TransactionsTestData.GetTransactions();
            var transactionsDaoMock = GetTransactionsDaoMockObjectToGetTransaction(transactions);

            var provider = new DbTransactionsProvider(transactionsDaoMock);

            var result = await provider.GetTransactionsBySkuAsync(transactions.First().Sku);
            result.Should().NotBeNull();
            result.First().Should().BeEquivalentTo(transactions.First());
        }

    }
}
