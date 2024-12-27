using CS306_Phase2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CS306_Phase2.Data;
using Microsoft.EntityFrameworkCore;

namespace CS306_Phase2.Controllers
{

    public class CustomerController : Controller
    {
        private readonly CargoDbContext _context;

        public CustomerController(CargoDbContext context)
        {
            _context = context;
        }

        // GET: Customer/Create
        public async Task<IActionResult> Create()
        {
            var companies = await _context.CargoCompany.ToListAsync();
            var viewModel = new AddCustomerViewModel
            {
                Companies = companies.Select(c => new SelectListItem
                {
                    Value = c.CompanyId.ToString(),
                    Text = c.CompanyName
                })
            };
            return View(viewModel);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCustomerViewModel model)
        {
            ModelState.Remove(nameof(model.Companies));
            if (ModelState.IsValid)
            {
                // Call stored procedure
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL AddNewCustomer({0}, {1}, {2}, {3})",
                    model.Name, model.Phone, model.CompanyId, model.Address);

                // Add a success message to TempData
                TempData["SuccessMessage"] = "Customer added successfully!";
                return View(model);
            }
            else
            {
                // Log ModelState errors for debugging
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                TempData["SuccessMessage"] = "Customer add failed!";
            }

            // Reload companies if model state is invalid
            model.Companies = (await _context.CargoCompany.ToListAsync())
                .Select(c => new SelectListItem
                {
                    Value = c.CompanyId.ToString(),
                    Text = c.CompanyName
                });
            return View(model);
        }


        // GET: Customer/Index
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customer.ToListAsync();
            return View(customers);
        }
    }

}
