using InvestmentTracker.Data;
using InvestmentTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
                  
                    _context.InvestmentLots.Add(investmentLot);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Investment lot added successfully!";
                    return RedirectToAction(nameof(Index));
                }

                
                return View(investmentLot);
            }
            catch (Exception ex)
            {
                // Manejo de errores
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
               
                var totalAvailableShares = _context.InvestmentLots.Sum(lot => lot.Shares);
                if (sharesToSell > totalAvailableShares)
                {
                    ModelState.AddModelError("", "Not enough shares available to sell.");
                    return View();
                }

                // FIFO: Process Sell
                var remainingShares = sharesToSell;
                decimal totalProfit = 0;
                decimal totalCostOfSoldShares = 0;
                int totalSoldShares = 0;

                foreach (var lot in _context.InvestmentLots.OrderBy(lot => lot.PurchaseDate))
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

               
                _context.SaveChanges();

                
                var remainingSharesAfterSale = _context.InvestmentLots.Sum(lot => lot.Shares);
                var totalCostOfRemainingShares = _context.InvestmentLots.Sum(lot => lot.Shares * lot.PricePerShare);
                decimal costBasisPerShareSold = totalCostOfSoldShares / totalSoldShares;
                decimal costBasisPerShareRemaining = remainingSharesAfterSale > 0
                    ? totalCostOfRemainingShares / remainingSharesAfterSale
                    : 0;

 
                TempData["SuccessMessage"] = $"Successfully sold {sharesToSell} shares.";
                TempData["RemainingShares"] = remainingSharesAfterSale;
                TempData["CostBasisSold"] = costBasisPerShareSold.ToString("F2");
                TempData["CostBasisRemaining"] = costBasisPerShareRemaining.ToString("F2");
                TempData["TotalProfit"] = totalProfit.ToString("F2");

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
