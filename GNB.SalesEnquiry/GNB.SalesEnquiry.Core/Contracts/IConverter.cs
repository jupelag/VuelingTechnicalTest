using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Core.Contracts;

public interface IConverter
{
    TransactionDto[] ConvertToEur(TransactionDto[] transactions,RateDto[] rates);
}