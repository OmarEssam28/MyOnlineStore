using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.Data;
using MyOnlineStore.Models;

namespace MyOnlineStore.Controllers
{
    public class UserController : Controller
    {
        // We create an instance of our context to talk to the database.
        private readonly MyContext _context = new MyContext();

        // GET: /User/Register
        // Returns the registration form view.
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /User/Register
        // Handles the submission of the registration form.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            // Server-side validation
            if (ModelState.IsValid)
            {
                // Check if email already exists
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "This email address is already in use.");
                    return View(user);
                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                
                // After successful registration, navigate to the login page.
                return RedirectToAction("Login");
            }
            // If model state is not valid, return the view with validation errors.
            return View(user);
        }

        // GET: /User/Login
        // Returns the login form view.
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /User/Login
        // Handles the submission of the login form.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Email and password are required.";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // After successful login, navigate to the products page.
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }
        }
    }
}
