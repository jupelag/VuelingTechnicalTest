using FluentAssertions;
using GNB.SalesEnquiry.RateCompleter;
using GNB.SalesEnquiry.Shared.Dtos;
using GNB.SalesEnquiry.Shared.Enums;
using Xunit;

namespace GNB.SalesEnquiry.Domain.Test.RateCompleterTest
{
    public class RateCalculatorTest
    {
        [Fact]
        public void Calculate_direct_rate()
        {
            var rateCalculator = new RateCalculator();
            var rates = RatesTestData.GetCompleteRates();
            const decimal expectedValue = 1.36m;

            var success =
                rateCalculator.CalculateDirectRate(rates, CurrencyTypesEnum.EUR, CurrencyTypesEnum.USD, out var value);
            success.Should().BeTrue();
            value.Should().Be(expectedValue);
        }
        [Fact]
        public void Calculate_direct_rate_no_data()
        {
            var rateCalculator = new RateCalculator();

            var success =
                rateCalculator.CalculateDirectRate(Array.Empty<RateDto>(), CurrencyTypesEnum.EUR, CurrencyTypesEnum.USD, out var value);
            success.Should().BeFalse();
            value.Should().Be(null);
        }
        [Fact]
        public void Calculate_indirect_rate()
        {
            var rateCalculator = new RateCalculator();
            var rates = RatesTestData.GetCompleteRates();

            const decimal expectedValue = 1.01m;

            var success =
                rateCalculator.CalculateIndirectRate(rates, CurrencyTypesEnum.USD, CurrencyTypesEnum.CAD, out var value);
            success.Should().BeTrue();
            value.Should().Be(expectedValue);
        }
        [Fact]
        public void Calculate_indirect_rate_no_data()
        {
            var rateCalculator = new RateCalculator();

            var success =
                rateCalculator.CalculateIndirectRate(Array.Empty<RateDto>(), CurrencyTypesEnum.USD, CurrencyTypesEnum.CAD, out var value);
            success.Should().BeFalse();
            value.Should().Be(null);
        }
        [Fact]
        public void Calculate_rate_directly()
        {
            var rateCalculator = new RateCalculator();
            var rates = RatesTestData.GetCompleteRates();
            const decimal expectedValue = 1.36m;

            var value = rateCalculator.CalculateRate(rates, CurrencyTypesEnum.EUR, CurrencyTypesEnum.USD);

            value.Should().Be(expectedValue);
        }
        [Fact]
        public void Calculate_rate_indirectly()
        {
            var rateCalculator = new RateCalculator();
            var rates = RatesTestData.GetCompleteRates();
            const decimal expectedValue = 1.01m;

            var value = rateCalculator.CalculateRate(rates, CurrencyTypesEnum.USD, CurrencyTypesEnum.CAD);

            value.Should().Be(expectedValue);
        }

        [Fact]
        public void Calculate_rate_with_no_data()
        {
            var rateCalculator = new RateCalculator();
            var value = rateCalculator.CalculateRate(Array.Empty<RateDto>(), CurrencyTypesEnum.USD, CurrencyTypesEnum.CAD);

            value.Should().BeNull();
        }
    }
}
