using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Core.Contracts
{
    public interface IProvider
    {
        Task<RateDto[]> GetRatesAsync();
        Task<TransactionDto[]> GetTransactionsAsync();
        Task<TransactionDto[]> GetTransactionsBySkuAsync(string sku);
    }
}
