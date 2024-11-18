using Microsoft.EntityFrameworkCore;
using InvestmentTracker.Models;

namespace InvestmentTracker.Data;

public class InvestmentDbContext : DbContext
{
    public DbSet<InvestmentLot> InvestmentLots { get; set; }

    public InvestmentDbContext(DbContextOptions<InvestmentDbContext> options) : base(options) { }
}
