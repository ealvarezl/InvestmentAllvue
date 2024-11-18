using InvestmentTracker.Data;
using InvestmentTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace InvestmentTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InvestmentDbContext _context;

        public HomeController(ILogger<HomeController> logger, InvestmentDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            try
            {
                
                var investmentLots = _context.InvestmentLots.ToList();

               
                return View(investmentLots);
            }
            catch (Exception ex)
            {
               
                _logger.LogError(ex, "Error recovering: Investment Lots.");

                
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InvestmentLot investmentLot)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    if (investmentLot.PurchaseDate > new DateTime(2024, 3, 31))
                    {
                        
                        TempData["WarningMessage"] = "The purchase date is after April 1, 2024. This lot may not be eligible for sales.";
                    }

                    
                    _context.InvestmentLots.Add(investmentLot);
                    _context.SaveChanges();

                    
                    TempData["SuccessMessage"] = "Investment lot added successfully!";
                    return RedirectToAction(nameof(Index));
                }

                return View(investmentLot);
            }
            catch (Exception ex)
            {
               
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View(investmentLot);
            }
        }



        // GET: Home/Sell
        public ActionResult Sell()
        {
           
            return View();
        }

        // POST: Home/Sell
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sell(int sharesToSell, decimal sellPricePerShare)
        {
            try
            {

                if (sharesToSell <= 0)
                {
                    ModelState.AddModelError("", "You must sell at least one share.");
                    return View();
                }

                // Valid lots to sell (Before march 31, 2024)
                var eligibleLots = _context.InvestmentLots
                    .Where(lot => lot.PurchaseDate <= new DateTime(2024, 3, 31))
                    .OrderBy(lot => lot.PurchaseDate)
                    .ToList();

 
                var totalEligibleShares = eligibleLots.Sum(lot => lot.Shares);
                if (sharesToSell > totalEligibleShares)
                {
                    ModelState.AddModelError("", "Not enough eligible shares available to sell. Only shares purchased before April 1, 2024, can be sold.");
                    return View();
                }

              
                var remainingShares = sharesToSell;
                decimal totalProfit = 0;
                decimal totalCostOfSoldShares = 0;
                int totalSoldShares = 0;

                //FIFO
                foreach (var lot in eligibleLots)
                {
                    if (remainingShares <= 0) break;

                    if (lot.Shares >= remainingShares)
                    {
                        totalProfit += remainingShares * (sellPricePerShare - lot.PricePerShare);
                        totalCostOfSoldShares += remainingShares * lot.PricePerShare;
                        totalSoldShares += remainingShares;
                        lot.Shares -= remainingShares;
                        remainingShares = 0;
                    }
                    else
                    {
                        totalProfit += lot.Shares * (sellPricePerShare - lot.PricePerShare);
                        totalCostOfSoldShares += lot.Shares * lot.PricePerShare;
                        totalSoldShares += lot.Shares;
                        remainingShares -= lot.Shares;
                        lot.Shares = 0;
                    }
                }

               
                if (totalSoldShares == 0)
                {
                    ModelState.AddModelError("", "No shares were sold. Please check your input.");
                    return View();
                }

                
                _context.SaveChanges();

              
                var remainingSharesAfterSale = _context.InvestmentLots.Sum(lot => lot.Shares);
                var totalCostOfRemainingShares = _context.InvestmentLots.Sum(lot => lot.Shares * lot.PricePerShare);

                decimal costBasisPerShareSold = Math.Round(totalCostOfSoldShares / totalSoldShares, 2);
                decimal costBasisPerShareRemaining = remainingSharesAfterSale > 0
                    ? Math.Round(totalCostOfRemainingShares / remainingSharesAfterSale, 2)
                    : 0;

              
                TempData["SuccessMessage"] = $"Successfully sold {sharesToSell} shares.";
                TempData["RemainingShares"] = $"Remaining Shares: {remainingSharesAfterSale} ";
                TempData["CostBasisSold"] = $"Cost Basis Per Sold Share: {costBasisPerShareSold.ToString("F2")} ";
                TempData["CostBasisRemaining"] = $"Cost Basis Per Remaining Share: {costBasisPerShareRemaining.ToString("F2")}";
                TempData["TotalProfit"] = $"Total Profit/Loss: {totalProfit.ToString("F2")}";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View();
            }
        }



    }
}
