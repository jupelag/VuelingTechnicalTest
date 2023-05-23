using GNB.SalesEnquiry.Core.Contracts;
using GNB.SalesEnquiry.Data.EF.SalesEnquiry.Context;
using GNB.SalesEnquiry.Data.EF.SalesEnquiry.Models;
using GNB.SalesEnquiry.Shared.Dtos;
using GNB.SalesEnquiry.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace GNB.SalesEnquiry.DataAccess
{
    public class RatesDao:IRatesDao, DbProvider.Contracts.IRatesDao
    {
        public async Task SaveRatesAsync(IEnumerable<RateDto> rateDtos)
        {
            try
            {
                await using var context = new SalesEnquiryContext();
                await context.Rates.ExecuteDeleteAsync();
                var rates = GetRates(rateDtos);
                await context.Rates.AddRangeAsync(rates);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving rates in db {ex.Message}", ex);
            }
        }
        private static Rates[] GetRates(IEnumerable<RateDto> rateDtos)
        {
            return rateDtos.Select(r => new Rates()
            {
                From = Enum.GetName(r.From),
                To = Enum.GetName(r.To),
                Rate = r.Rate
            }).ToArray();
        }

        public async Task<RateDto[]> GetRatesAsync()
        {
            try
            {
                await using var context = new SalesEnquiryContext();
                var rates = context.Rates;
                return GetDtos(rates);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting rates from db {ex.Message}", ex);
            }
        }

        private static RateDto[] GetDtos(IEnumerable<Rates> rates)
        {
            return rates.Select(r => new RateDto()
            {
                From = Enum.Parse<CurrencyTypesEnum>(r.From),
                To = Enum.Parse<CurrencyTypesEnum>(r.To),
                Rate = r.Rate ?? decimal.Zero
            }).ToArray();
        }
    }
}
