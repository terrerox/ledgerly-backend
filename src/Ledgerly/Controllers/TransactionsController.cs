using Ledgerly.Dtos;
using Ledgerly.Models;
using Ledgerly.Models.Enums;
using Ledgerly.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ledgerly.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController(ITransactionService transactionService)
{
    [HttpGet]
    public async Task<IEnumerable<Transaction>> GetAll(DateTime? startDate, DateTime? endDate, TransactionType? type, decimal? minAmount, decimal? maxAmount)
    {
        return await transactionService.GetAll(startDate, endDate, type, minAmount, maxAmount);
    }
    
    [HttpGet("summary")]
    public async Task<Summary> GetSummary(DateTime? startDate, DateTime? endDate, TransactionType? type, decimal? minAmount, decimal? maxAmount)
    {
        return await transactionService.GetSummary(startDate, endDate, type, minAmount, maxAmount);
    }
    
    [HttpPost]
    public async Task<Transaction> Create(CreateTransaction transaction)
    {
        return await transactionService.Create(transaction);
    }
}