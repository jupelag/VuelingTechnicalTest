using GNB.SalesEnquiry.Core.Contracts;
using GNB.SalesEnquiry.Provider.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Provider
{
    public class Provider:IProvider
    {
        private readonly IApiRatesProvider _apiRatesProvider;
        private readonly IDbRatesProvider _dbRatesProvider;
        private readonly IApiTransactionsProvider _apiTransactionsProvider;
        private readonly IDbTransactionsProvider _dbTransactionsProvider;

        public Provider(IApiRatesProvider apiRatesProvider, IDbRatesProvider dbRatesProvider,
            IApiTransactionsProvider apiTransactionsProvider, IDbTransactionsProvider dbTransactionsProvider)
        {
            _apiRatesProvider = apiRatesProvider;
            _dbRatesProvider = dbRatesProvider;
            _apiTransactionsProvider = apiTransactionsProvider;
            _dbTransactionsProvider = dbTransactionsProvider;
        }

        public async Task<RateDto[]> GetRatesAsync()
        {
            try
            {
                return await _apiRatesProvider.GetRatesAsync();
            }
            catch (Exception e)
            {
                try
                {
                    return await _dbRatesProvider.GetRatesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        $"Error in provider: rates could not be retrieved. Api error: {e.Message}. Db error:{ex.Message}");
                }
            }
        }

        public async Task<TransactionDto[]> GetTransactionsAsync()
        {
            try
            {
                var transactions = await _apiTransactionsProvider.GetTransactionsAsync();
                return transactions ?? Array.Empty<TransactionDto>();
            }
            catch (Exception e)
            {
                try
                {
                    return await _dbTransactionsProvider.GetTransactionsAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        $"Error in provider: transactions could not be retrieved. Api error: {e.Message}. Db error:{ex.Message}");
                }
            }
        }

        public async Task<TransactionDto[]> GetTransactionsBySkuAsync(string sku)
        {
            try
            {
                return await _apiTransactionsProvider.GetTransactionsBySkuAsync(sku);
            }
            catch (Exception e)
            {
                try
                {
                    return await _dbTransactionsProvider.GetTransactionsBySkuAsync(sku);
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        $"Error in provider: transactions could not be retrieved. Api error: {e.Message}. Db error:{ex.Message}");
                }
            }
        }
    }
}