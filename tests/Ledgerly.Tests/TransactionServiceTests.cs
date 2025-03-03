using Ledgerly.Dtos;
using Ledgerly.Models;
using Ledgerly.Models.Enums;
using Ledgerly.Persistence;
using Ledgerly.Services;
using Microsoft.EntityFrameworkCore;

namespace Ledgerly.Tests
{
    [TestFixture]
    public class TransactionServiceTests
    {
        private Context _context;
        private TransactionService _transactionService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new Context(options);
            _transactionService = new TransactionService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up the in-memory database after each test
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Create_ValidTransaction_ReturnsCreatedTransaction()
        {
            var newTransaction = new CreateTransaction
            (
                "Test transaction",
                100.50m,
                new DateTime(2025, 3, 3),
                TransactionType.Income
            );

            var result = await _transactionService.Create(newTransaction);

            Assert.IsNotNull(result);
            Assert.AreEqual(newTransaction.Description, result.Description);
            Assert.AreEqual(newTransaction.Amount, result.Amount);
            Assert.AreEqual(newTransaction.Type, result.Type);

            var transactionInDb = await _context.Transactions
                .FirstOrDefaultAsync(t => t.Description == "Test transaction");
            Assert.IsNotNull(transactionInDb);
            Assert.AreEqual(newTransaction.Amount, transactionInDb.Amount);
        }

        [Test]
        public async Task GetAll_WithFilters_ReturnsFilteredTransactions()
        {
            var transaction1 = new Transaction 
            { 
                Description = "Income 1", 
                Amount = 100, 
                Date = new DateTime(2025, 1, 1), 
                Type = TransactionType.Income 
            };
            var transaction2 = new Transaction 
            { 
                Description = "Expense 1", 
                Amount = 50, 
                Date = new DateTime(2025, 2, 1), 
                Type = TransactionType.Expense 
            };
            var transaction3 = new Transaction 
            { 
                Description = "Income 2", 
                Amount = 200, 
                Date = new DateTime(2025, 3, 1), 
                Type = TransactionType.Income 
            };

            _context.Transactions.AddRange(transaction1, transaction2, transaction3);
            await _context.SaveChangesAsync();

            var results = await _transactionService.GetAll(
                new DateTime(2025, 1, 1),
                new DateTime(2025, 3, 1),
                TransactionType.Income,
                null,
                null);

            var filteredTransactions = results.ToList();
            Assert.AreEqual(2, filteredTransactions.Count);
            Assert.AreEqual("Income 1", filteredTransactions.First().Description);
        }

        [Test]
        public async Task GetSummary_ReturnsCorrectSummary()
        {
            var transaction1 = new Transaction 
            { 
                Description = "Income 1", 
                Amount = 100, 
                Date = new DateTime(2025, 1, 1), 
                Type = TransactionType.Income 
            };
            var transaction2 = new Transaction 
            { 
                Description = "Income 2", 
                Amount = 200, 
                Date = new DateTime(2025, 2, 1), 
                Type = TransactionType.Income 
            };
            var transaction3 = new Transaction 
            { 
                Description = "Expense 1", 
                Amount = 50, 
                Date = new DateTime(2025, 3, 1), 
                Type = TransactionType.Expense 
            };
            var transaction4 = new Transaction 
            { 
                Description = "Expense 2", 
                Amount = 30, 
                Date = new DateTime(2025, 4, 1), 
                Type = TransactionType.Expense 
            };

            _context.Transactions.AddRange(transaction1, transaction2, transaction3, transaction4);
            await _context.SaveChangesAsync();

            var summary = await _transactionService.GetSummary(null, null, null, null, null);

            Assert.AreEqual(300, summary.Income);
            Assert.AreEqual(80, summary.Expenses);
            Assert.AreEqual(220, summary.Balance); // 300 - 80 = 220
        }
    }
}
