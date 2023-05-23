using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using GNB.SalesEnquiry.ApiProvider;
using GNB.SalesEnquiry.ApiProvider.Contracts;
using Moq;
using Xunit;

namespace GNB.SalesEnquiry.Domain.Test.ApiProviderTest
{
    public class ApiRateProviderTest
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
        public async Task GetRatesAsync_successful_response()
        {
            var rates = RatesTestData.GetCompleteRates();
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(rates),Encoding.UTF8, "application/json")
            };
            var httpClient = GetApiRateProviderHttpClient(expectedResponse);
            var config = GetConfigMockObjet();

            var provider = new ApiRateProvider(httpClient, config);

            var result = await provider.GetRatesAsync();
            result.Should().NotBeNull();
            result.Length.Should().Be(rates.Length);
            result.Should().BeEquivalentTo(rates);
        }

        [Fact]
        public async Task GetRatesAsync_unsuccessful_response()
        {

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var httpClient = GetApiRateProviderHttpClient(expectedResponse);
            var config = GetConfigMockObjet();

            var provider = new ApiRateProvider(httpClient, config);

            await FluentActions.Awaiting(() => provider.GetRatesAsync()).Should().ThrowAsync<HttpRequestException>();
        }

        [Fact]
        public async Task GetRatesAsync_when_deserialization_fails()
        {
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[{'invalid json: true'}]", Encoding.UTF8, "application/json")
            };
            var httpClient = GetApiRateProviderHttpClient(expectedResponse);
            var config = GetConfigMockObjet();

            var provider = new ApiRateProvider(httpClient, config);

            await FluentActions.Awaiting(() => provider.GetRatesAsync()).Should().ThrowAsync<Exception>();

        }
    }
}
