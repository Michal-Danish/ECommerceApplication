using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ECommerce.Data
{
    public class ProductsRepository
    {
        private string _connectionString; 

        public ProductsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Category> GetCategories()
        {
            using (var context = new CommerceContext(_connectionString))
            {
                return context.Categories.ToList();
            }
        }

        public IEnumerable<Product> GetProductsForCategory(int catId)
        {
            using (var context = new CommerceContext(_connectionString))
            {
                return context.Products.Where(p => p.CategoryId == catId).ToList();
            }
        }

        public string GetCategoryName(int catId)
        {
            using (var context = new CommerceContext(_connectionString))
            {
                return context.Categories.FirstOrDefault(c => c.Id == catId).Name;
            }
        }

        public Product GetProductById(int id)
        {
            using (var context = new CommerceContext(_connectionString))
            {
                return context.Products.FirstOrDefault(p => p.Id == id);
            }
        }

        public bool AddProductToCart(ShoppingCartProducts scp)
        {
            using (var context = new CommerceContext(_connectionString))
            {
                ShoppingCartProducts prod = context.ShoppingCartsProducts.
                    FirstOrDefault(p => p.ShoppingCartId == scp.ShoppingCartId && p.ProductId == scp.ProductId);
                if(prod == null)
                {
                    context.ShoppingCartsProducts.Add(scp);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int AddCart()
        {
            ShoppingCart sc = new ShoppingCart
            {
                DateCreated = System.DateTime.Now
            };
            using (var context = new CommerceContext(_connectionString))
            {
                context.ShoppingCarts.Add(sc);
                context.SaveChanges();
                return sc.Id;
            }
        }

        public IEnumerable<ShoppingCartProducts> GetProductsForCart(int cartId)
        {
            using (var context = new CommerceContext(_connectionString))
            {
                return context.ShoppingCartsProducts.Where(i => i.ShoppingCartId == cartId).Include(p => p.Product).ToList();
            }
        }

        public void DeleteProduct(int productId, int shoppingId)
        {
            using (var context = new CommerceContext(_connectionString))
            {
                context.Database.ExecuteSqlCommand("DELETE FROM ShoppingCartsProducts WHERE ProductId = @productId AND ShoppingCartId = @shoppingId",
                        new SqlParameter("@productId", productId),
                        new SqlParameter("@shoppingId", shoppingId));
                context.SaveChanges();
            }
        }

        public void UpdateCart(int productId, int shoppingId, int quantity)
        {
            using (var context = new CommerceContext(_connectionString))
            {
                context.Database.ExecuteSqlCommand("UPDATE ShoppingCartsProducts SET Quantity = @quantity WHERE ProductId = @productId AND ShoppingCartId = @shoppingId",
                    new SqlParameter("@quantity", quantity),
                    new SqlParameter("@productId", productId),
                    new SqlParameter("@shoppingId", shoppingId));
                context.SaveChanges();
            }
        }

        public void PlaceOrder(Order o)
        {
            using (var context = new CommerceContext(_connectionString))
            {
                context.Orders.Add(o);
                context.SaveChanges();
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            using (var context = new CommerceContext(_connectionString))
            {
                IEnumerable<Order> orders = context.Orders.Include(o => o.ShoppingCart).ThenInclude(sc => sc.ShoppingCartProducts).
                    ThenInclude(scp => scp.Product).Where(i => i.Filled != true).ToList();
                foreach(Order order in orders)
                {
                    decimal total = 0;
                    IEnumerable<ShoppingCartProducts> products = GetProductsForCart(order.ShoppingCartId);
                    foreach(ShoppingCartProducts scp in products)
                    {
                        total += scp.Product.Price * scp.Quantity;
                    }
                    order.OrderTotal = total;
                }
                return orders;
            }
        }

        public Order GetOrder(int orderId)
        {
            using (var context = new CommerceContext(_connectionString))
            {
                Order order = context.Orders.Include(o => o.ShoppingCart).ThenInclude(sc => sc.ShoppingCartProducts).
                    ThenInclude(scp => scp.Product).FirstOrDefault(or => or.Id == orderId);
                decimal total = 0;
                IEnumerable<ShoppingCartProducts> products = GetProductsForCart(order.ShoppingCartId);
                foreach (ShoppingCartProducts scp in products)
                {
                    total += scp.Product.Price * scp.Quantity;
                }
                order.OrderTotal = total;
                return order;
            }
        }

        public void FillOrder(int orderId)
        {
            using (var context = new CommerceContext(_connectionString))
            {
                context.Database.ExecuteSqlCommand("UPDATE Orders SET Filled = '1' WHERE Id = @orderId",
                    new SqlParameter("@orderId", orderId));
                context.SaveChanges();
            }
        }

        public void AddProduct(Product p)
        {
            using (var context = new CommerceContext(_connectionString))
            {
                context.Products.Add(p);
                context.SaveChanges();
            }
        }

        public void AddCategory(Category c)
        {
            using (var context = new CommerceContext(_connectionString))
            {
                context.Categories.Add(c);
                context.SaveChanges();
            }
        }

    }
}
