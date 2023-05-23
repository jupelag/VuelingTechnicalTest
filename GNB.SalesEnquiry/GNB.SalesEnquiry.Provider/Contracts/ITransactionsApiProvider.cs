using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Provider.Contracts
{
    public interface IApiTransactionsProvider
    {
        Task<TransactionDto[]> GetTransactionsBySkuAsync(string sku);
        Task<TransactionDto[]?> GetTransactionsAsync();
    }
}
