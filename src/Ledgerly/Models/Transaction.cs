using Ledgerly.Models.Enums;

namespace Ledgerly.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public DateTimeOffset Date { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
}