using GNB.SalesEnquiry.ApiProvider.Contracts;
using GNB.SalesEnquiry.Provider.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.ApiProvider
{
    public class ApiTransactionsProvider:ApiProviderBase, IApiTransactionsProvider
    {
        public ApiTransactionsProvider(IApiProviderHttpClient client, IApiProviderConfig config) : base(client, config)
        { }

        public async Task<TransactionDto[]> GetTransactionsBySkuAsync(string sku)
        {
            var transactions = await base.GetAsync<TransactionDto>(_config.RatesEndPoint);
            return transactions.Where(t=>t.Sku.Equals(sku,StringComparison.InvariantCultureIgnoreCase)).ToArray();
        }

        public async Task<TransactionDto[]?> GetTransactionsAsync()
        {
            var transactions = await base.GetAsync<TransactionDto>(_config.RatesEndPoint);
            return transactions;
        }
    }
}
