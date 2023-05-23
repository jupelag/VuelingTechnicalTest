using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Core.Contracts
{
    public interface IRatesDao
    {
        Task SaveRatesAsync(IEnumerable<RateDto> rateDtos);
    }
}
