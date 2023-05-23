using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Core.Contracts;

public interface ICompleter
{
    RateDto[] CompleteRates(RateDto[] rates);
}