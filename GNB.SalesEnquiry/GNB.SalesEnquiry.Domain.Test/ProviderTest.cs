using FluentAssertions;
using GNB.SalesEnquiry.Provider.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;
using Moq;
using Xunit;

namespace GNB.SalesEnquiry.Domain.Test;

public class ProviderTests
{
    [Fact]
    public async Task GetRatesAsync_when_api_success()
    {
        var apiRatesProviderMock = new Mock<IApiRatesProvider>();
        var rates = new[] { new RateDto() };
        apiRatesProviderMock.Setup(x => x.GetRatesAsync()).ReturnsAsync(rates);

        var dbRatesProviderMock = new Mock<IDbRatesProvider>();

        var provider = new Provider.Provider(apiRatesProviderMock.Object, dbRatesProviderMock.Object,
            Mock.Of<IApiTransactionsProvider>(), Mock.Of<IDbTransactionsProvider>());

        var result = await provider.GetRatesAsync();

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(rates);
    }

    [Fact]
    public async Task GetRatesAsync_when_api_fails_and_dbSucceeds()
    {
        
        var apiRatesProviderMock = new Mock<IApiRatesProvider>();
        apiRatesProviderMock.Setup(x => x.GetRatesAsync()).ThrowsAsync(new Exception());

        var dbRatesProviderMock = new Mock<IDbRatesProvider>();
        var rates = new[] { new RateDto() };
        dbRatesProviderMock.Setup(x => x.GetRatesAsync()).ReturnsAsync(rates);

        var provider = new Provider.Provider(apiRatesProviderMock.Object, dbRatesProviderMock.Object,
            Mock.Of<IApiTransactionsProvider>(), Mock.Of<IDbTransactionsProvider>());

        
        var result = await provider.GetRatesAsync();
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(rates);
    }

    [Fact]
    public async Task GetRatesAsync_ShouldThrowException_WhenApiAndDbFail()
    {
        var apiError = "Api Error";
        var dbError = "DB Error";

        var apiRatesProviderMock = new Mock<IApiRatesProvider>();
        apiRatesProviderMock.Setup(x => x.GetRatesAsync()).ThrowsAsync(new Exception(apiError));

        var dbRatesProviderMock = new Mock<IDbRatesProvider>();
        dbRatesProviderMock.Setup(x => x.GetRatesAsync()).ThrowsAsync(new Exception(dbError));

        var provider = new Provider.Provider(apiRatesProviderMock.Object, dbRatesProviderMock.Object,
            Mock.Of<IApiTransactionsProvider>(), Mock.Of<IDbTransactionsProvider>());

        var res = await FluentActions.Awaiting(() => provider.GetRatesAsync()).Should().ThrowAsync<Exception>();
        res.And.Message.Should().Contain(apiError);
        res.And.Message.Should().Contain(dbError);
    }
    [Fact]
    public async Task GetTransactionsAsync_when_api_success()
    {
        
        var transactions = new[] { new TransactionDto() };

        var apiTransactionsProviderMock = new Mock<IApiTransactionsProvider>();
        apiTransactionsProviderMock.Setup(x => x.GetTransactionsAsync()).ReturnsAsync(transactions);

        var provider = new Provider.Provider(Mock.Of<IApiRatesProvider>(), Mock.Of<IDbRatesProvider>(),
            apiTransactionsProviderMock.Object, Mock.Of<IDbTransactionsProvider>());

        var result = await provider.GetTransactionsAsync();

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(transactions);
    }

    [Fact]
    public async Task GetTransactionsAsync_when_api_fails_and_dbSucceeds()
    {
        var transactions = new[] { new TransactionDto() };
        var apiTransactionsProviderMock = new Mock<IApiTransactionsProvider>();
        apiTransactionsProviderMock.Setup(x => x.GetTransactionsAsync()).ThrowsAsync(new Exception());
        var dbTransactionsProviderMock = new Mock<IDbTransactionsProvider>();
        dbTransactionsProviderMock.Setup(x => x.GetTransactionsAsync()).ReturnsAsync(transactions);

        var provider = new Provider.Provider(Mock.Of<IApiRatesProvider>(), Mock.Of<IDbRatesProvider>(),
            apiTransactionsProviderMock.Object, dbTransactionsProviderMock.Object);

        var result = await provider.GetTransactionsAsync();
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(transactions);
    }

    [Fact]
    public async Task GetTransactionsAsync_when_api_and_db_fails()
    {
        var apiError = "Api Error";
        var dbError = "DB Error";

        var apiTransactionsProviderMock = new Mock<IApiTransactionsProvider>();
        apiTransactionsProviderMock.Setup(x => x.GetTransactionsAsync()).ThrowsAsync(new Exception(apiError));

        var dbTransactionsProviderMock = new Mock<IDbTransactionsProvider>();
        dbTransactionsProviderMock.Setup(x => x.GetTransactionsAsync()).ThrowsAsync(new Exception(dbError));

        var provider = new Provider.Provider(Mock.Of<IApiRatesProvider>(), Mock.Of<IDbRatesProvider>(),
            apiTransactionsProviderMock.Object, dbTransactionsProviderMock.Object);

        var result = await FluentActions.Awaiting(() => provider.GetTransactionsAsync()).Should().ThrowAsync<Exception>();
        result.And.Message.Should().Contain(apiError);
        result.And.Message.Should().Contain(dbError);
    }
    [Fact]
    public async Task GetTransactionBySkuAsync_when_api_success()
    {
        var apiTransactionsProviderMock = new Mock<IApiTransactionsProvider>();
        var expectedTransactionDto = new[] {new TransactionDto()};

        apiTransactionsProviderMock.Setup(x => x.GetTransactionsBySkuAsync(It.IsAny<string>())).ReturnsAsync(expectedTransactionDto);

        var provider = new Provider.Provider(Mock.Of<IApiRatesProvider>(), Mock.Of<IDbRatesProvider>(),
                apiTransactionsProviderMock.Object, Mock.Of<IDbTransactionsProvider>());

        var result = await provider.GetTransactionsBySkuAsync(It.IsAny<string>());

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedTransactionDto);
    }

    [Fact]
    public async Task GetTransactionsBySkuAsync_when_api_fails_and_dbSucceeds()
    {
        var apiTransactionsProviderMock = new Mock<IApiTransactionsProvider>();
        apiTransactionsProviderMock.Setup(x => x.GetTransactionsBySkuAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
        
        var expectedTransactionDto = new[] { new TransactionDto() };
        var dbTransactionsProviderMock = new Mock<IDbTransactionsProvider>();
        dbTransactionsProviderMock.Setup(x => x.GetTransactionsBySkuAsync(It.IsAny<string>())).ReturnsAsync(expectedTransactionDto);
        var provider = new Provider.Provider(Mock.Of<IApiRatesProvider>(), Mock.Of<IDbRatesProvider>(),
          apiTransactionsProviderMock.Object, dbTransactionsProviderMock.Object);

        var result = await provider.GetTransactionsBySkuAsync(It.IsAny<string>());

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedTransactionDto);
    }

    [Fact]
    public async Task GetTransactionBySkuAsync_when_api_and_db_fails()
    {

        var apiTransactionsProviderMock = new Mock<IApiTransactionsProvider>();
        var dbTransactionsProviderMock = new Mock<IDbTransactionsProvider>();
        const string apiError = "Api Error";
        const string dbError = "DB Error";

        apiTransactionsProviderMock.Setup(x => x.GetTransactionsBySkuAsync(It.IsAny<string>())).ThrowsAsync(new Exception(apiError));

        dbTransactionsProviderMock.Setup(x => x.GetTransactionsBySkuAsync(It.IsAny<string>())).ThrowsAsync(new Exception(dbError));


        var provider = new Provider.Provider(Mock.Of<IApiRatesProvider>(), Mock.Of<IDbRatesProvider>(),
            apiTransactionsProviderMock.Object, dbTransactionsProviderMock.Object);

        var result = await FluentActions.Awaiting(() => provider.GetTransactionsBySkuAsync(It.IsAny<string>())).Should().ThrowAsync<Exception>();
        result.And.Message.Should().Contain(apiError);
        result.And.Message.Should().Contain(dbError);
    }
}