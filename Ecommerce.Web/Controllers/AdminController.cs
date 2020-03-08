using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IHostingEnvironment _environment;
        private string _connectionString;

        public AdminController(IConfiguration configuration, IHostingEnvironment environment)
        {
            _environment = environment;
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            var repo = new ProductsRepository(_connectionString);
            IEnumerable<Category> categories = repo.GetCategories();
            return View(categories);
        }

        [HttpPost]
        public IActionResult AddProduct(Product p, IFormFile imageFile)
        {
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            string fullPath = Path.Combine(_environment.WebRootPath, "uploads", fileName);
            using (FileStream stream = new FileStream(fullPath, FileMode.CreateNew))
            {
                imageFile.CopyTo(stream);
            }

            p.ImageFileName = fileName;
            var repo = new ProductsRepository(_connectionString);
            repo.AddProduct(p);
            return RedirectToAction("Index");
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category c)
        {
            var repo = new ProductsRepository(_connectionString);
            repo.AddCategory(c);
            return RedirectToAction("Index");
        }

        public IActionResult ViewOrders()
        {
            var repo = new ProductsRepository(_connectionString);
            IEnumerable<Order> orders = repo.GetOrders();
            return View(orders);
        }

        [HttpPost]
        public IActionResult ViewOrder(int orderId)
        {
            var repo = new ProductsRepository(_connectionString);
            Order o = repo.GetOrder(orderId);
            return View(o);
        }

        [HttpPost]
        public IActionResult FillOrder(int orderId)
        {
            var repo = new ProductsRepository(_connectionString);
            repo.FillOrder(orderId);
            return RedirectToAction("ViewOrders");
        }

    }
}