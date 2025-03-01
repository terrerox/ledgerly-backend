using Ledgerly.Models.Enums;

namespace Ledgerly.Dtos;

public record CreateTransaction(string Description, decimal Amount, DateTime Date, TransactionType Type);