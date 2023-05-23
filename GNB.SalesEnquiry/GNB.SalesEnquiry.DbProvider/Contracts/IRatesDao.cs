using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.DbProvider.Contracts
{
    public interface IRatesDao
    {
        Task<RateDto[]> GetRatesAsync();
    }
}
