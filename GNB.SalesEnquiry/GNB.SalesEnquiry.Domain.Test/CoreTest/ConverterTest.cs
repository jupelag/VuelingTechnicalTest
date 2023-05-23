using FluentAssertions;
using GNB.SalesEnquiry.Core;
using GNB.SalesEnquiry.Shared.Dtos;
using GNB.SalesEnquiry.Shared.Enums;
using Xunit;

namespace GNB.SalesEnquiry.Domain.Test.CoreTest;

public class ConverterTests
{
    [Fact]
    public void ConvertToEur_when_given_eur_transactions()
    {
        var transactions = new[]
        {
            new TransactionDto { Currency = CurrencyTypesEnum.EUR, Amount = 10 },
            new TransactionDto { Currency = CurrencyTypesEnum.EUR, Amount = 20 }
        };
        var rates = new[]
        {
            new RateDto { From = CurrencyTypesEnum.EUR, To = CurrencyTypesEnum.USD, Rate = 1.2m },
            new RateDto { From = CurrencyTypesEnum.USD, To = CurrencyTypesEnum.EUR, Rate = 0.8m }
        };

        var converter = new Converter();

        var result = converter.ConvertToEur(transactions, rates);

        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(transactions);
    }

    [Fact]
    public void ConvertToEur_when_given_non_eur_transactions()
    {
        var transactions = new[]
        {
            new TransactionDto { Currency = CurrencyTypesEnum.USD, Amount = 10 },
            new TransactionDto { Currency = CurrencyTypesEnum.CAD, Amount = 20 }
        };
        var rates = new[]
        {
            new RateDto { From = CurrencyTypesEnum.EUR, To = CurrencyTypesEnum.USD, Rate = 1.2m },
            new RateDto { From = CurrencyTypesEnum.USD, To = CurrencyTypesEnum.EUR, Rate = 0.8m },
            new RateDto { From = CurrencyTypesEnum.CAD, To = CurrencyTypesEnum.EUR, Rate = 1.4m },
            new RateDto { From = CurrencyTypesEnum.EUR, To = CurrencyTypesEnum.CAD, Rate = 0.7m }
        };

        var converter = new Converter();

        var result = converter.ConvertToEur(transactions,rates).ToArray();

        result.Should().HaveCount(2);
        result[0].Currency.Should().Be(CurrencyTypesEnum.EUR);
        result[0].Amount.Should().Be(8m);
        result[1].Currency.Should().Be(CurrencyTypesEnum.EUR);
        result[1].Amount.Should().Be(28m);
    }
}