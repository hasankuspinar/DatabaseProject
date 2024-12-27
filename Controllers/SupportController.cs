using Microsoft.AspNetCore.Mvc;

namespace CS306_Phase2.Controllers
{
    public class SupportController : Controller
    {
        // GET: /Support/UserChat
        [HttpGet]
        public IActionResult UserChat()
        {

            return View();
        }

        // GET: /Support/AdminChat
        [HttpGet]
        public IActionResult AdminChat()
        {

            return View();
        }
    }
}

