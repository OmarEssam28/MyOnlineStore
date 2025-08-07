using Microsoft.AspNetCore.Mvc;
using MyOnlineStore.Data;
using MyOnlineStore.Models;

namespace MyOnlineStore.Controllers
{
    public class UserController : Controller
    {
      
        private readonly MyContext _context = new MyContext();

       
      
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            
            if (ModelState.IsValid)
            {
                
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "This email address is already in use.");
                    return View(user);
                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                
                
                return RedirectToAction("Login");
            }
            
            return View(user);
        }

        
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

       
        
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
