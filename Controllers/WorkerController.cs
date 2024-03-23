using KorvSolutions.Models;
using Microsoft.AspNetCore.Mvc;

namespace KorvSolutions.Controllers
{
    public class WorkerController : Controller
    {
        public IActionResult AddProduct(string datum)
        {
            // Visa produkter för angivet datum för uppvägning
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(string produktNamn, List<Ingredient> ingredients)
        {
            // Lägg till råvaror för produkten och skapa batch
            return RedirectToAction("AddProduct");
        }

        public IActionResult CreateBatch(string produktNamn)
        {
            // Skapa batch och visa batchnummer
            return RedirectToAction("CreateBatch");
        }
    }
}
