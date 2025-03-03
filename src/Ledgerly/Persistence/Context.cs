using Ledgerly.Models;
using Microsoft.EntityFrameworkCore;

namespace Ledgerly.Persistence;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Transaction> Transactions { get; set; }
}
