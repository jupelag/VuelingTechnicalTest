using GNB.SalesEnquiry.Core.Contracts;
using GNB.SalesEnquiry.Data.EF.SalesEnquiry.Context;
using GNB.SalesEnquiry.Data.EF.SalesEnquiry.Models;
using GNB.SalesEnquiry.Shared.Dtos;
using GNB.SalesEnquiry.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace GNB.SalesEnquiry.DataAccess
{
    public class TransactionsDao : ITransactionsDao, DbProvider.Contracts.ITransactionsDao
    {
        public async Task SaveTransactionsAsync(IEnumerable<TransactionDto> transactionDtos)
        {
            try
            {
                await using var context = new SalesEnquiryContext();
                await context.Transactions.ExecuteDeleteAsync();
                var transactions = GetTransacions(transactionDtos);
                await context.Transactions.AddRangeAsync(transactions);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving transactions in db {ex.Message}", ex);
            }
        }

        private static Transactions[] GetTransacions(IEnumerable<TransactionDto> transactionDtos)
        {
            return transactionDtos.Select(t => new Transactions()
            {
                Sku = t.Sku,
                Amount = t.Amount,
                Currency = Enum.GetName(t.Currency)
            }).ToArray();
        }

        public async Task<TransactionDto[]> GetTransactionAsync()
        {
            try
            {
                await using var context = new SalesEnquiryContext();
                var transactions = context.Transactions;
                return GetTransactions(transactions);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting transactions from db {ex.Message}", ex);
            }
        }

        public async Task<TransactionDto[]> GetTransactionsBySku(string sku)
        {
            try
            {
                await using var context = new SalesEnquiryContext();
                var transactions = context.Transactions.Where(t=>t.Sku.Equals(sku,StringComparison.InvariantCultureIgnoreCase)).ToArray();
                return GetTransactions(transactions);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting transactions from db {ex.Message}", ex);
            }
        }
        private static TransactionDto[] GetTransactions(IEnumerable<Transactions> transactions)
        {
            return transactions.Select(r => new TransactionDto()
            {
                Sku = r.Sku,
                Currency = Enum.Parse<CurrencyTypesEnum>(r.Currency),
                Amount = r.Amount ?? decimal.Zero
            }).ToArray();
        }
    }
}
