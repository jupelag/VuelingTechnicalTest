using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Provider.Contracts
{
    public interface IDbTransactionsProvider
    {
        Task<TransactionDto[]> GetTransactionsAsync();
        Task<TransactionDto[]> GetTransactionsBySkuAsync(string sku);
    }
}
