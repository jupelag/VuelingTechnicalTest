using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Provider.Contracts
{
    public interface IApiRatesProvider
    {
        Task<RateDto[]> GetRatesAsync();
        Task<T[]> GetAsync<T>(string url);
    }
}
