using FluentAssertions;
using GNB.SalesEnquiry.RateCompleter;
using GNB.SalesEnquiry.RateCompleter.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;
using GNB.SalesEnquiry.Shared.Enums;
using Moq;
using Xunit;

namespace GNB.SalesEnquiry.Domain.Test.RateCompleterTest
{
    public class CompleterTest
    {
        private static IRateCalculator GetRateCalculatorMockObject(decimal? returnedValue)
        {
            var rateCalculator = new Mock<IRateCalculator>();
            rateCalculator.Setup(r => r.CalculateRate(It.IsAny<RateDto[]>(), It.IsAny<CurrencyTypesEnum>(),
                It.IsAny<CurrencyTypesEnum>())).Returns(returnedValue);
            return rateCalculator.Object;
        }

        [Fact]
        public Task GetConversionStrategies()
        {
            const decimal expectedValue = 1.36m;
            var rates = RatesTestData.GetCompleteRates();
            var rateCalculatorMockObject = GetRateCalculatorMockObject(expectedValue);
            var completer = new Completer(rateCalculatorMockObject);

            var result = completer.CompleteRates(rates);

            result.First(s => s is { From: CurrencyTypesEnum.EUR, To: CurrencyTypesEnum.USD }).Rate
                .Should().Be(expectedValue);
            return Task.CompletedTask;
        }

        [Fact]
        public Task GetConversionStrategies_catch_exception()
        {
            var expectedException = new InvalidOperationException("exc message");

            var rates = RatesTestData.GetCompleteRates();
            
            var rateCalculatorMock = new Mock<IRateCalculator>();
            rateCalculatorMock.Setup(r => r.CalculateRate(It.IsAny<RateDto[]>(), It.IsAny<CurrencyTypesEnum>(),
                It.IsAny<CurrencyTypesEnum>())).Throws(expectedException);

            var completer = new Completer(rateCalculatorMock.Object);

            FluentActions.Invoking(() => completer.CompleteRates(rates))
                .Should().Throw<Exception>()
                .WithMessage("Error calculating conversion strategies: exc message");
            return Task.CompletedTask;
        }
    }
}
