using Ledgerly.Dtos;
using Ledgerly.Models;
using Ledgerly.Models.Enums;

namespace Ledgerly.Services;

public interface ITransactionService
{
    Task<Transaction> Create(CreateTransaction newTransaction);
    Task<IEnumerable<Transaction>> GetAll(DateTime? startDate, DateTime? endDate, TransactionType? type, decimal? minAmount, decimal? maxAmount);
    Task<Summary> GetSummary(DateTime? startDate, DateTime? endDate, TransactionType? type, decimal? minAmount, decimal? maxAmount);
}