using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Provider.Contracts
{
    public interface IDbRatesProvider
    {
        Task<RateDto[]> GetRatesAsync();
    }
}
