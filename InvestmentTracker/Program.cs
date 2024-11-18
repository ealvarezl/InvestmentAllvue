using InvestmentTracker.Data;
using Microsoft.EntityFrameworkCore;
using InvestmentTracker.Utilities;

var builder = WebApplication.CreateBuilder(args);

//EF Core for MySQL
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
    dbContext.Database.Migrate();
    DataSeeder.Seed(dbContext);
}


app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();