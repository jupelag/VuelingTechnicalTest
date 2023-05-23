using GNB.SalesEnquiry.RateCompleter.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;
using GNB.SalesEnquiry.Shared.Enums;

namespace GNB.SalesEnquiry.RateCompleter;

public class RateCalculator : IRateCalculator
{
    public decimal? CalculateRate(IEnumerable<RateDto> rates, CurrencyTypesEnum fromCurrency, CurrencyTypesEnum toCurrency)
    {
        return CalculateRate(rates, fromCurrency, toCurrency, new HashSet<CurrencyTypesEnum>() { fromCurrency });
    }

    public bool CalculateIndirectRate(IEnumerable<RateDto> rates, CurrencyTypesEnum fromCurrency, CurrencyTypesEnum toCurrency,
        out decimal? indirectRateValue)
    {
        indirectRateValue = CalculateRate(rates, fromCurrency, toCurrency, new HashSet<CurrencyTypesEnum>() { fromCurrency });
        return indirectRateValue != null;
    }

    private decimal? CalculateRate(IEnumerable<RateDto> rates, CurrencyTypesEnum fromCurrency, CurrencyTypesEnum toCurrency, ISet<CurrencyTypesEnum> visitedCurrencies)
    {            
        if (CalculateDirectRate(rates, fromCurrency, toCurrency, out var directRateValue)) return directRateValue;
        if (CalculateIndirectRate(rates, fromCurrency, toCurrency, visitedCurrencies, out var indirectRateValue)) return indirectRateValue;
        return null;
    }

    private bool CalculateIndirectRate(IEnumerable<RateDto> rates, CurrencyTypesEnum fromCurrency, CurrencyTypesEnum toCurrency,
        ISet<CurrencyTypesEnum> visitedCurrencies, out decimal? indirectRateValue)
    {
        try
        {
            indirectRateValue = null;
            foreach (var rate in rates.Where(r => r.From == fromCurrency && !visitedCurrencies.Contains(r.To)))
            {
                visitedCurrencies.Add(rate.To);
                var indirectFactor = CalculateRate(rates, rate.To, toCurrency, visitedCurrencies);
                if (!(indirectFactor > 0)) continue;

                indirectRateValue = rate.Rate * indirectFactor;
                indirectRateValue = Math.Round(indirectRateValue.Value, 2, MidpointRounding.ToEven);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error calculating indirect rate {ex.Message}", ex);
        }
    }

    public bool CalculateDirectRate(IEnumerable<RateDto> rates, CurrencyTypesEnum fromCurrency, CurrencyTypesEnum toCurrency,
        out decimal? directRateValue)
    {
        try
        {
            directRateValue = null;
            var directRate = rates.FirstOrDefault(r => r.From == fromCurrency && r.To == toCurrency);
            if (directRate == null) return false;

            directRateValue = directRate.Rate;
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error calculating direct rate {ex.Message}", ex);
        }
    }
}