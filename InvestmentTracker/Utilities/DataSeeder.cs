using InvestmentTracker.Data;
using InvestmentTracker.Models;

namespace InvestmentTracker.Utilities
{

    public static class DataSeeder
    {
        public static void Seed(InvestmentDbContext context)
        {
            
            if (!context.InvestmentLots.Any())
            {
                context.InvestmentLots.AddRange(
                    new InvestmentLot { Shares = 100, PricePerShare = 20, PurchaseDate = new DateTime(2024, 4, 5) },
                    new InvestmentLot { Shares = 150, PricePerShare = 30, PurchaseDate = new DateTime(2024, 4, 21) },
                    new InvestmentLot { Shares = 120, PricePerShare = 10, PurchaseDate = new DateTime(2024, 4, 28) }
                );
                context.SaveChanges();
            }
        }
    }

}