using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Core.Contracts
{
    public interface IRatesProvider
    {
        Task<RateDto[]> GetRatesAsync();
    }
}