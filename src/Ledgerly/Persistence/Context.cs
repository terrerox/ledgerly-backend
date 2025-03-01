using Ledgerly.Models;
using Microsoft.EntityFrameworkCore;

namespace Ledgerly.Persistence;

public class Context(IConfiguration configuration) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(configuration.GetConnectionString("ledgerly"));
    }

    public DbSet<Transaction> Transactions { get; set; }
}
