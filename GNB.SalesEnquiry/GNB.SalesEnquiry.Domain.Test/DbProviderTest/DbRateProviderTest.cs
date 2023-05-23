using FluentAssertions;
using GNB.SalesEnquiry.DbProvider;
using GNB.SalesEnquiry.DbProvider.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;
using Moq;
using Xunit;

namespace GNB.SalesEnquiry.Domain.Test.DbProviderTest
{
    public class DbRateProviderTest
    {
        private IRatesDao GetRateDaoMockObject(RateDto[] expectedResponse)
        {
            var mock = new Mock<IRatesDao>();
            mock.Setup(m => m.GetRatesAsync()).Returns(Task.FromResult(expectedResponse));
            return mock.Object;
        }

        [Fact]
        public async Task GetRateAsync()
        {
            var rates = RatesTestData.GetCompleteRates();
            var rateDao = GetRateDaoMockObject(rates);
            var provider = new DbRatesProvider(rateDao);

            var result = await provider.GetRatesAsync();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(rates);
        }

        [Fact]
        public async Task GetRateAsync_when_dao_throws_exception()
        {
            var rateDaoMock = new Mock<IRatesDao>();
            rateDaoMock.Setup(m => m.GetRatesAsync()).Throws<Exception>();
            var provider = new DbRatesProvider(rateDaoMock.Object);
            await FluentActions.Awaiting(() => provider.GetRatesAsync()).Should().ThrowAsync<Exception>();
        }
    }
}
