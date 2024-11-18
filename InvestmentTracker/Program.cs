using InvestmentTracker.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<InvestmentDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 31))
    )
);

builder.Services.AddControllersWithViews();
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InvestmentDbContext>();
    var retryCount = 5; 
    var delay = TimeSpan.FromSeconds(5); 

    for (int i = 0; i < retryCount; i++)
    {
        try
        {
            
            Console.WriteLine("Attempting to connect to the database...");
            dbContext.Database.CanConnect();
            Console.WriteLine("Successfully connected to the database.");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to connect to the database. Retrying... ({i + 1}/{retryCount})");
            Console.WriteLine($"Error: {ex.Message}");

            if (i == retryCount - 1)
            {
                
                Console.WriteLine("Failed to connect to the database after multiple retries.");
                throw;
            }

        
            Thread.Sleep(delay);
        }
    }
}


app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
