using GNB.SalesEnquiry.DbProvider.Contracts;
using GNB.SalesEnquiry.Provider.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.DbProvider
{


    public class DbRatesProvider : IDbRatesProvider
    {
        private readonly IRatesDao _rateDao;

        public DbRatesProvider(IRatesDao rateDao)
        {
            _rateDao = rateDao;
        }

        public async Task<RateDto[]> GetRatesAsync()
        {
            try
            {
                var rates = await _rateDao.GetRatesAsync();
                return rates;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting rates from db: {ex.Message}");
            }
        }
    }
}
