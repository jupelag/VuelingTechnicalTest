using GNB.SalesEnquiry.Shared.Dtos;
using GNB.SalesEnquiry.Shared.Enums;

namespace GNB.SalesEnquiry.RateCompleter.Contracts
{
    public interface IRateCalculator
    {
        decimal? CalculateRate(IEnumerable<RateDto> rates, CurrencyTypesEnum fromCurrency, CurrencyTypesEnum toCurrency);

        bool CalculateIndirectRate(IEnumerable<RateDto> rates, CurrencyTypesEnum fromCurrency, CurrencyTypesEnum toCurrency,
            out decimal? indirectRateValue);

        bool CalculateDirectRate(IEnumerable<RateDto> rates, CurrencyTypesEnum fromCurrency, CurrencyTypesEnum toCurrency,
            out decimal? directRateValue);
    }
}
