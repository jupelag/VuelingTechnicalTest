using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Core.Contracts
{
    public interface ITransactionsDao
    {
        Task SaveTransactionsAsync(IEnumerable<TransactionDto> transactionDtos);
    }
}
