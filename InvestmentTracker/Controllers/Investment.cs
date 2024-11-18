using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentTracker.Controllers
{
    public class Investment : Controller
    {
        // GET: Investment
        public ActionResult Index()
        {
            return View();
        }

        // GET: Investment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Investment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Investment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Investment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Investment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Investment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Investment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
