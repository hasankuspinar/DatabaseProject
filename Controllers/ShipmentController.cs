using CS306_Phase2.Models;
using Microsoft.AspNetCore.Mvc;
using CS306_Phase2.Data;
using Microsoft.EntityFrameworkCore;
namespace CS306_Phase2.Controllers
{
    public class ShipmentController : Controller
    {
        private readonly CargoDbContext _context;

        public ShipmentController(CargoDbContext context)
        {
            _context = context;
        }

        // GET: Shipment/ByCustomer/5
        public async Task<IActionResult> ByCustomer(int id)
        {
            var shipments = await _context.Set<ShipmentViewModel>()
                .FromSqlRaw(@"
                CALL GetCustomerShipments({0})", id)
                .ToListAsync();

            return View(shipments);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteShipment(int shipmentId)
        {
            try
            {
                // Call the stored procedure to delete the shipment
                await _context.Database.ExecuteSqlRawAsync("CALL DeleteShipment({0})", shipmentId);

                // Add success message
                TempData["SuccessMessage"] = "Shipment deleted successfully!";
            }
            catch (Exception ex)
            {
                // Log the error and add a failure message
                Console.WriteLine(ex.Message);
                TempData["ErrorMessage"] = "Failed to delete shipment. Please check the Shipment ID.";
            }

            // Redirect to the DeleteShipment view
            return RedirectToAction(nameof(DeleteShipment));
        }

        [HttpGet]
        public IActionResult DeleteShipment()
        {
            return View("DeleteShipment");
        }

    }
}
