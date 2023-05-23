using GNB.SalesEnquiry.Core.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;
using Grpc.Core;

namespace GNB.SalesEnquiry.gRPCApi.Services
{
    public class SalesEnquiryService: SalesEnquiry.SalesEnquiryBase
    {
        private readonly ILogger<SalesEnquiryService> _logger;
        private readonly ISalesEnquiry _salesEnquiry;

        public SalesEnquiryService(ILogger<SalesEnquiryService> logger,ISalesEnquiry salesEnquiry)
        {
            _logger = logger;
            _salesEnquiry = salesEnquiry;
        }

        public override async Task<RateDtos> GetRatesAsync(EmptyMessage request, ServerCallContext context)
        {
            
            try
            {
                var ratesGetted = await _salesEnquiry.GetRatesAsync();
                var rates = ratesGetted.Select(r => new RateDto
                {
                    From = Enum.GetName(r.From),
                    To = Enum.GetName(r.To),
                    Rate = Convert.ToDouble(r.Rate)
                });
                var rateDtos = new RateDtos();
                rateDtos.Rates.AddRange(rates);

                return rateDtos;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error geting rates: {e.Message}");
            }

            return new RateDtos();
        }

        public override async Task<TransactionDtos> GetTransactionsAsync(EmptyMessage request, ServerCallContext context)
        {
            try
            {
                var transactionsGetted = await _salesEnquiry.GetTransactionsAsync();
                var transactions = transactionsGetted.Select(t => new TransactionDto
                {
                    Sku = t.Sku,
                    Amount = Convert.ToDouble(t.Amount),
                    Currency = Enum.GetName(t.Currency)
                });

                var transactionDtos = new TransactionDtos();
                transactionDtos.Transactions.AddRange(transactions);

                return transactionDtos;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error geting transactions: {e.Message}");
            }
            return new TransactionDtos();
        }
        public override async Task<TransactionDtos> GetTransactionsInEurBySkuAsync(sku request, ServerCallContext context)
        {
            try
            {
                var transactionsGetted = await _salesEnquiry.GetTransactionsInEurBySkuAsync(request.Sku);
                var transactions = transactionsGetted.Select(t => new TransactionDto
                {
                    Sku = t.Sku,
                    Amount = Convert.ToDouble(t.Amount),
                    Currency = Enum.GetName(t.Currency)
                });

                var transactionDtos = new TransactionDtos();
                transactionDtos.Transactions.AddRange(transactions);

                return transactionDtos;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error geting transactions in eur by sku: {e.Message}");
            }
            return new TransactionDtos();
        }
        public override async Task<amount> GetTotalTransactionsAmountInEurAsync(EmptyMessage request, ServerCallContext context)
        {
            try
            {
                var amount = await _salesEnquiry.GetTotalTransactionsAmountInEurAsync();
                if (amount == null)
                {
                    return new amount
                    {
                        Amount = 0
                    };
                }

                return new amount
                {
                    Amount = Convert.ToDouble(amount.Value)
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error geting transactions amount in eur: {e.Message}");
            }

            return new amount { };
        }
    }
}
