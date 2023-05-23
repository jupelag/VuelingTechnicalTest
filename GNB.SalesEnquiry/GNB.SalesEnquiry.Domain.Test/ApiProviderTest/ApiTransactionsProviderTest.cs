using FluentAssertions;
using GNB.SalesEnquiry.ApiProvider;
using GNB.SalesEnquiry.ApiProvider.Contracts;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace GNB.SalesEnquiry.Domain.Test.ApiProviderTest
{
    public class ApiTransactionsProviderTest
    {
        private IApiProviderConfig GetConfigMockObjet()
        {
            var mock = new Mock<IApiProviderConfig>();
            mock.Setup(m => m.RatesEndPoint).Returns("url");
            return mock.Object;
        }

        private IApiProviderHttpClient GetApiRateProviderHttpClient(HttpResponseMessage expectedResponse)
        {
            var mock = new Mock<IApiProviderHttpClient>();
            mock.Setup(m => m.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(expectedResponse));
            return mock.Object;
        }
        [Fact]
        public async Task GetTransactionsAsync_successful_response()
        {
            var transactions = TransactionsTestData.GetTransactions();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(transactions), Encoding.UTF8, "application/json")
            };
            var httpClient = GetApiRateProviderHttpClient(expectedResponse);
            var config = GetConfigMockObjet();

            var provider = new ApiTransactionsProvider(httpClient, config);

            var result = await provider.GetTransactionsAsync();
            result.Should().NotBeNull();
            result!.Length.Should().Be(transactions.Length);
            result.Should().BeEquivalentTo(transactions);
        }

        [Fact]
        public async Task GetTransactionsAsync_unsuccessful_response()
        {

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var httpClient = GetApiRateProviderHttpClient(expectedResponse);
            var config = GetConfigMockObjet();

            var provider = new ApiTransactionsProvider(httpClient, config);

            await FluentActions.Awaiting(() => provider.GetTransactionsAsync()).Should().ThrowAsync<HttpRequestException>();
        }

        [Fact]
        public async Task GetTransactionsAsync_when_deserialization_fails()
        {
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[{'invalid json: true'}]", Encoding.UTF8, "application/json")
            };
            var httpClient = GetApiRateProviderHttpClient(expectedResponse);
            var config = GetConfigMockObjet();

            var provider = new ApiTransactionsProvider(httpClient, config);

            await FluentActions.Awaiting(() => provider.GetTransactionsAsync()).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task GetTransactionBySkuAsync()
        {
            var transactions = TransactionsTestData.GetTransactions();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(transactions), Encoding.UTF8, "application/json")
            };
            var httpClient = GetApiRateProviderHttpClient(expectedResponse);
            var config = GetConfigMockObjet();

            var provider = new ApiTransactionsProvider(httpClient, config);

            var result = await provider.GetTransactionsBySkuAsync(transactions.First().Sku);
            result.Should().NotBeNull();
            result.First().Should().BeEquivalentTo(transactions.First());
        }

        [Fact]
        public async Task GetTransactionBySkuAsync_no_result()
        {
            var transactions = TransactionsTestData.GetTransactions();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(transactions), Encoding.UTF8, "application/json")
            };
            var httpClient = GetApiRateProviderHttpClient(expectedResponse);
            var config = GetConfigMockObjet();

            var provider = new ApiTransactionsProvider(httpClient, config);

            var result = await provider.GetTransactionsBySkuAsync("1234");
            result.Should().BeEmpty();
        }
    }
}
