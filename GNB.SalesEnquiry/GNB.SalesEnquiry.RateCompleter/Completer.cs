using GNB.SalesEnquiry.Core.Contracts;
using GNB.SalesEnquiry.RateCompleter.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;
using GNB.SalesEnquiry.Shared.Enums;

namespace GNB.SalesEnquiry.RateCompleter
{
    public class Completer : ICompleter
    {
        private readonly IRateCalculator _rateCalculator;

        public Completer(IRateCalculator rateCalculator)
        {
            _rateCalculator = rateCalculator;
        }

        public RateDto[] CompleteRates(RateDto[] rates)
        {
            try
            {
                var currencyTypes = Enum.GetValues(typeof(CurrencyTypesEnum)).Cast<CurrencyTypesEnum>().ToList();
                var newRates = (from fromCurrency
                            in currencyTypes
                        from toCurrency
                            in currencyTypes
                        where toCurrency != fromCurrency
                        let rate = _rateCalculator.CalculateRate(rates, fromCurrency, toCurrency)
                        where rate != null
                        select new RateDto(fromCurrency, toCurrency, rate.Value))
                    .ToArray();
                return newRates;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calculating conversion strategies: {ex.Message}", ex);
            }
        }
    }
}