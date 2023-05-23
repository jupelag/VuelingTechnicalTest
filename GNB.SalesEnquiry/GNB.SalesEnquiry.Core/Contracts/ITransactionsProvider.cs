using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Core.Contracts
{
    public interface ITransactionsProvider
    {
        public Task<TransactionDto[]> GetTransactionsAsync();
        public Task<TransactionDto[]> GetTransactionsBySkuAsync(string sku);
    }
}
