using KorvSolutions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace KorvSolutions.Controllers
{
    public class BossController : Controller
    {
        private readonly AppDbContext _context;

        public BossController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult CreatePlan()
        {
            // Retrieve the list of products from the database
            var availableProducts = _context.Products.ToList();

            // Pass the list of products to the view
            return View(availableProducts);
        }

        [HttpPost]
        public IActionResult CreatePlan(DateTime datum, Dictionary<string, int> quantities)
        {
            // Create a new plan instance
            var plan = new Plan
            {
                Date = datum,
                PlanProducts = new List<PlanProduct>()
            };

            // Iterate through the selected products and quantities
            foreach (var productName in quantities.Keys)
            {
                int quantity = quantities[productName];

                // Only add the product to the plan if the quantity is greater than zero
                if (quantity > 0)
                {
                    // Find or create the product
                    var product = _context.Products.FirstOrDefault(p => p.Name == productName);

                    if (product != null)
                    {
                        var planProduct = new PlanProduct
                        {
                            Id = product.Id,
                            PlanId = plan.Id,
                            ProductId = product.Id,
                            Quantity = quantity
                        };

                        // Add the plan-product association to the plan's list
                        plan.PlanProducts.Add(planProduct);
                    }
                }
            }

            // Save the changes to the database
            _context.Plans.Add(plan);
            _context.SaveChanges();

            return RedirectToAction(nameof(ViewPlan));
        }

        public IActionResult ViewPlan()
        {
            // Retrieve all plans including their associated plan-products and products
            List<Plan> plans = _context.Plans
                .Include(p => p.PlanProducts)
                    .ThenInclude(pp => pp.Product)
                .OrderBy(p => p.Date)
                .ToList();

            return View(plans);
        }


        // GET: Boss/EditPlan/5
        public IActionResult EditPlan(int id)
        {
            var plan = _context.Plans
                              .Include(p => p.PlanProducts)  // Include plan products
                                  .ThenInclude(pp => pp.Product) // Include associated products
                              .FirstOrDefault(p => p.Id == id);

            return View(plan);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPlan(int id, Plan updatedPlan)
        {
            // Retrieve the existing plan from the database, including its plan products
            var existingPlan = _context.Plans
                                       .Include(p => p.PlanProducts)
                                       .FirstOrDefault(p => p.Id == id);

            // Check if updatedPlan.PlanProducts is null or empty
            if (updatedPlan.PlanProducts != null && updatedPlan.PlanProducts.Any())
            {
                // Update existing plan products' quantities and remove those not in the updated plan
                foreach (var existingPlanProduct in existingPlan.PlanProducts.ToList())
                {
                    // Find the corresponding plan product in the updated plan
                    var updatedPlanProduct = updatedPlan.PlanProducts.FirstOrDefault(pp => pp.Id == existingPlanProduct.Id);

                    if (updatedPlanProduct != null)
                    {
                        // Update the quantity of the existing plan product
                        existingPlanProduct.Quantity = updatedPlanProduct.Quantity;
                        _context.SaveChanges();

                        if (updatedPlanProduct.Quantity == 0)
                        {
                            // If quantity is zero, remove the plan product
                            _context.Remove(existingPlanProduct);
                        }
                    }
                }
            }

            // Save changes to the database
            _context.SaveChanges();

            // Redirect to the ViewPlan action
            return RedirectToAction(nameof(ViewPlan));
        }










        // GET: Boss/CreateProduct
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(CreatePlan));
        }


        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(CreatePlan));
        }



        // GET: Boss/DeletePlan/5
        public IActionResult DeletePlan(int id)
        {
            var plan = _context.Plans.Find(id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // POST: Boss/DeletePlan/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePlanConfirmed(int id)
        {
            var plan = _context.Plans.Find(id);
            if (plan != null)
            {
                _context.Plans.Remove(plan);
                _context.SaveChanges();
            }

            // Redirect to the ViewPlan action method
            return RedirectToAction(nameof(ViewPlan));
        }


        // GET: Boss/DeleteProduct/5
        public IActionResult DeletePlanProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Boss/DeleteProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePlanProductConfirmed(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            // Redirect to the ViewPlan action method or any other appropriate action
            return RedirectToAction(nameof(ViewPlan));
        }




        private bool PlanExists(int id)
        {
            return _context.Plans.Any(p => p.Id == id);
        }
    }
}