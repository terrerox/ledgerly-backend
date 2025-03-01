using Ledgerly.Dtos;
using Ledgerly.Models;
using Ledgerly.Models.Enums;
using Ledgerly.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ledgerly.Services;

public class TransactionService : ITransactionService
{
    Context _context;

    public TransactionService(Context context)
    {
        _context = context;
    }

    public async Task<Transaction> Create(CreateTransaction newTransaction)
    {
        // AutoMapper would be a better fit here, but because is only one class, it's not worth it
        var transaction = new Transaction
        {
            Description = newTransaction.Description,
            Amount = newTransaction.Amount,
            Date = newTransaction.Date,
            Type = newTransaction.Type
        };
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
    public async Task<IEnumerable<Transaction>> GetAll(DateTime? startDate, DateTime? endDate, TransactionType? type, decimal? minAmount, decimal? maxAmount)
    {
        var filteredQuery = ApplyFilters(startDate, endDate, type, minAmount, maxAmount);
        return await filteredQuery.ToListAsync();
    }

    public async Task<Summary> GetSummary(DateTime? startDate, DateTime? endDate, TransactionType? type, decimal? minAmount, decimal? maxAmount)
    {
        var filteredQuery = ApplyFilters(startDate, endDate, type, minAmount, maxAmount);
        var groupedSums = await filteredQuery
            .GroupBy(t => t.Type)
            .Select(g => new { Type = g.Key, Total = g.Sum(t => t.Amount) })
            .ToListAsync();

        var totalIncome = groupedSums
            .Where(g => g.Type == TransactionType.Income)
            .Select(g => g.Total)
            .FirstOrDefault();

        var totalExpenses = groupedSums
            .Where(g => g.Type == TransactionType.Expense)
            .Select(g => g.Total)
            .FirstOrDefault();

        var balance = totalIncome - totalExpenses;

        return new Summary(balance, totalIncome, totalExpenses);
    }
    private IQueryable<Transaction> ApplyFilters(DateTime? startDate, DateTime? endDate, TransactionType? type, decimal? minAmount, decimal? maxAmount)
    {
        var query = _context.Transactions.AsQueryable();
        if (startDate != null)
        {
            query = query.Where(t => t.Date >= startDate);
        }
        if (endDate != null)
        {
            query = query.Where(t => t.Date <= endDate);
        }
        if (type != null)
        {
            query = query.Where(t => t.Type == type);
        }
        if (minAmount != null)
        {
            query = query.Where(t => t.Amount >= minAmount);
        }
        if (maxAmount != null)
        {
            query = query.Where(t => t.Amount <= maxAmount);
        }
        return query;
    }
}