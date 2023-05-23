using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.DbProvider.Contracts
{
    public interface ITransactionsDao
    {
        Task<TransactionDto[]> GetTransactionAsync();
        Task<TransactionDto[]> GetTransactionsBySku(string sku);
    }
}
