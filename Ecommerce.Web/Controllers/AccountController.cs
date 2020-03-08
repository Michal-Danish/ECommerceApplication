using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerce.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Web.Controllers
{
    public class AccountController : Controller
    {

        private string _connectionString;

        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, string companyPassword)
        {
            var repo = new UserRepository(_connectionString);
            var user = repo.Login(email, password);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if(companyPassword == "Electronics613")
            {
                var claims = new List<Claim>
                {
                    new Claim("user", email)
                };
                HttpContext.SignInAsync(new ClaimsPrincipal(
                    new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            }
            return Redirect("/admin/index");
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(User user, string password, string companyPassword)
        {
            if (companyPassword == "Electronics613")
            {
                var repo = new UserRepository(_connectionString);
                repo.AddUser(user, password);
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Signup");
        }
        
    }
}