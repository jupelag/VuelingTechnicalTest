using GNB.SalesEnquiry.ApiProvider;
using System.Text.Json;
using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Domain.Test
{
    public static class TransactionsTestData
    {
        private static TransactionDto[] GetTransactionsData(string json) => JsonSerializer.Deserialize<TransactionDto[]>(json);

        public static TransactionDto[] GetTransactions()
        {
            const string json = @"[
 { ""sku"": ""T2006"", ""amount"": ""10.00"", ""currency"": ""USD"" },
 { ""sku"": ""M2007"", ""amount"": ""34.57"", ""currency"": ""CAD"" },
 { ""sku"": ""R2008"", ""amount"": ""17.95"", ""currency"": ""USD"" },
 { ""sku"": ""T2006"", ""amount"": ""7.63"", ""currency"": ""EUR"" },
 { ""sku"": ""B2009"", ""amount"": ""21.23"", ""currency"": ""USD"" }
]";
            return GetTransactionsData(json);
        }
    }
}
