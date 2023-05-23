using GNB.SalesEnquiry.ApiProvider.Contracts;
using GNB.SalesEnquiry.Provider.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.ApiProvider
{


    public class ApiRateProvider : ApiProviderBase, IApiRatesProvider
    {
        public ApiRateProvider(IApiProviderHttpClient client, IApiProviderConfig config):base(client,config)
        { }

        public async Task<RateDto[]> GetRatesAsync()
        {
            var rates = await base.GetAsync<RateDto>(_config.RatesEndPoint) as RateDto[];
            return rates;
        }
    }
}
