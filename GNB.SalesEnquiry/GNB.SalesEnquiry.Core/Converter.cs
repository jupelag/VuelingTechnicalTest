using GNB.SalesEnquiry.Core.Contracts;
using GNB.SalesEnquiry.Shared.Dtos;
using GNB.SalesEnquiry.Shared.Enums;

namespace GNB.SalesEnquiry.Core
{
    public class Converter :IConverter
    {
        public TransactionDto[] ConvertToEur(TransactionDto[] transactions, RateDto[] rates)
        {
            var list = new List<TransactionDto>();
            foreach (var transaction in transactions)
            {
                if (transaction.Currency == CurrencyTypesEnum.EUR)
                {
                    list.Add(transaction);
                    continue;
                }

                var rate = rates.FirstOrDefault(r=>r.From == transaction.Currency && r.To == CurrencyTypesEnum.EUR);
                if (rate == null)
                {
                    continue;
                }

                transaction.Amount *= rate.Rate;
                transaction.Currency = CurrencyTypesEnum.EUR;
                list.Add(transaction);
            }
            return list.ToArray();
        }
    }
}
