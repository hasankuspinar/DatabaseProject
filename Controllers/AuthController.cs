using CS306_Phase2.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace CS306_Phase2.Controllers
{
    public class AuthController : Controller
    {
        private readonly FirestoreDb _firestoreDb;

        public AuthController()
        {
            string projectId = "cs306phase3-a045a";
            var credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "firebase-service-account.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);

            _firestoreDb = FirestoreDb.Create(projectId);
        }

        // GET: Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Fetch user data from Firestore
            Query query = _firestoreDb.Collection("users").WhereEqualTo("username", model.Username);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

            if (querySnapshot.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            DocumentSnapshot userDoc = querySnapshot.Documents.First();
            var userData = userDoc.ConvertTo<User>();

            // Check password (no hashing in this case)
            if (userData.Password != model.Password)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            // Redirect based on user role
            if (userData.UserRole == "user")
            {
                // Pass username and userRole via query parameters
                return RedirectToAction("UserChat", "Support", new { username = userData.Username, role = "user" });
            }
            else 
            {
                // Pass username and userRole via query parameters
                return RedirectToAction("AdminChat", "Support", new { username = userData.Username, role = "admin" });
            }

        }

    }
}

