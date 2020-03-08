using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Web.Models;
using Microsoft.Extensions.Configuration;
using ECommerce.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Ecommerce.Web.Controllers
{

    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new ProductsRepository(_connectionString);
            IEnumerable<Category> categories = repo.GetCategories();
            return View(categories);
        }

        public IActionResult ProductsOfCategory(int catId)
        {
            var repo = new ProductsRepository(_connectionString);
            IEnumerable<Product> products = repo.GetProductsForCategory(catId);
            string catName = repo.GetCategoryName(catId);
            ProductsViewModel vm = new ProductsViewModel
            {
                Products = products,
                CategoryName = catName
            };
            return View(vm);
        }

        public IActionResult IndividualProduct(int proId)
        {
            var repo = new ProductsRepository(_connectionString);
            Product p = repo.GetProductById(proId);
            IndividualProductViewModel vm = new IndividualProductViewModel
            {
                Product = p
            };
            if (HttpContext.Session.Get<int>("cartId") != 0)
            {
                vm.CartId = HttpContext.Session.Get<int>("cartId");
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddProductToCart(int productId, int quantity, int cartId)
        {
            var repo = new ProductsRepository(_connectionString);
            if (cartId != 0)
            {
                ShoppingCartProducts scp = new ShoppingCartProducts
                {
                    ProductId = productId,
                    ShoppingCartId = cartId,
                    Quantity = quantity
                };
                bool b = repo.AddProductToCart(scp);
                return Json(b);
            }
            else
            {
                int cId = repo.AddCart();
                ShoppingCartProducts scp = new ShoppingCartProducts
                {
                    ProductId = productId,
                    ShoppingCartId = cId,
                    Quantity = quantity
                };
                bool b = repo.AddProductToCart(scp);
                HttpContext.Session.Set("cartId", cId);
                return Json(b);
            }
        }

        public IActionResult ViewCart()
        {
            var repo = new ProductsRepository(_connectionString);
            int cartId = HttpContext.Session.Get<int>("cartId");
            IEnumerable<ShoppingCartProducts> products = repo.GetProductsForCart(cartId);
            return View(products);
        }

        [HttpPost]
        public IActionResult Delete(int productId, int shoppingId)
        {
            var repo = new ProductsRepository(_connectionString);
            repo.DeleteProduct(productId, shoppingId);
            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult UpdateCart(int productId, int shoppingId, int quantity)
        {
            var repo = new ProductsRepository(_connectionString);
            repo.UpdateCart(productId, shoppingId, quantity);
            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult PlaceOrder(decimal total)
        {
            int cartId = HttpContext.Session.Get<int>("cartId");
            return View(cartId);
        }

        [HttpPost]
        public IActionResult AddOrder(Order o)
        {
            var repo = new ProductsRepository(_connectionString);
            repo.PlaceOrder(o);
            HttpContext.Session.Set("cartId", 0);
            return Redirect("/");
        }

    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}