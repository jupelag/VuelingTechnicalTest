using GNB.SalesEnquiry.Core.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;
using Array = System.Array;

namespace GNB.SalesEnquiry.Core
{
    public class SalesEnquiry : ISalesEnquiry
    {
        private readonly IProvider _provider;
        private readonly IRatesDao _ratesDao;
        private readonly ITransactionsDao _transactionsDao;
        private readonly ICompleter _rateCompleter;
        private readonly IConverter _converter;

        public SalesEnquiry(IProvider provider, IRatesDao ratesDao, ITransactionsDao transactionsDao, ICompleter rateCompleter, IConverter converter)
        {
            _provider = provider;
            _ratesDao = ratesDao;
            _transactionsDao = transactionsDao;
            _rateCompleter = rateCompleter;
            _converter = converter;
        }
        public async Task<RateDto[]> GetRatesAsync()
        {
            var rates = await _provider.GetRatesAsync();

            if (!rates.Any()) return Array.Empty<RateDto>();

            await _ratesDao.SaveRatesAsync(rates);

            return rates;
        }

        public async Task<TransactionDto[]> GetTransactionsAsync()
        {
            var transactions = await _provider.GetTransactionsAsync();

            if (!transactions.Any()) return Array.Empty<TransactionDto>();

            await _transactionsDao.SaveTransactionsAsync(transactions);

            return transactions;
        }

        public async Task<TransactionDto[]> GetTransactionsInEurBySkuAsync(string sku)
        {
            var rates = await GetRatesAsync();
            if(!rates.Any()) return Array.Empty<TransactionDto>();

            var transactions = await GetTransactionsAsync();

            var transactionsBySku =
                transactions.Where(t => t.Sku.Equals(sku, StringComparison.InvariantCultureIgnoreCase)).ToArray();
            if (!transactionsBySku.Any()) return Array.Empty<TransactionDto>();

            var completedRates = _rateCompleter.CompleteRates(rates);

            var transactionsInEur = _converter.ConvertToEur(transactionsBySku, completedRates);
            return transactionsInEur.ToArray();

        }

        public async Task<decimal?> GetTotalTransactionsAmountInEurAsync()
        {
            var rates = await GetRatesAsync();
            if (!rates.Any()) return null;

            var transactions = await GetTransactionsAsync();
            if (!transactions.Any()) return null;

            var completedRates = _rateCompleter.CompleteRates(rates);

            var transactionsInEur = _converter.ConvertToEur(transactions, completedRates);
            return transactionsInEur.Sum(t=>t.Amount);
        }

    }
}
