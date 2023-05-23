using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Core.Contracts
{
    public interface ISalesEnquiry
    {
        Task<RateDto[]> GetRatesAsync();
        Task<TransactionDto[]> GetTransactionsAsync();
        Task<TransactionDto[]> GetTransactionsInEurBySkuAsync(string sku);
        Task<decimal?> GetTotalTransactionsAmountInEurAsync();
    }
}
