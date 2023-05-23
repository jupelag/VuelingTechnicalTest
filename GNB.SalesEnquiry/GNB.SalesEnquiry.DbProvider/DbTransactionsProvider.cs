using GNB.SalesEnquiry.Core.Contracts;
using GNB.SalesEnquiry.Provider.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;
using ITransactionsDao = GNB.SalesEnquiry.DbProvider.Contracts.ITransactionsDao;

namespace GNB.SalesEnquiry.DbProvider
{


    public class DbTransactionsProvider:ITransactionsProvider, IDbTransactionsProvider
    {
        private readonly ITransactionsDao _transactionDao;

        public DbTransactionsProvider(ITransactionsDao transactionDao)
        {
            _transactionDao = transactionDao;
        }

        public async Task<TransactionDto[]> GetTransactionsAsync()
        {
            try
            {
                var transactions = await _transactionDao.GetTransactionAsync();
                return transactions;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting transactions from db: {ex.Message}");
            }

            
        }

        public async Task<TransactionDto[]> GetTransactionsBySkuAsync(string sku)
        {
            try
            {
                var transaction = await _transactionDao.GetTransactionsBySku(sku);
                return transaction;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting transactions from db: {ex.Message}");
            }
        }
    }
}
