using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data
{
    public class CommerceContext : DbContext
    {
        private string _connectionString;
        public CommerceContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCartProducts>()
               .HasKey(sp => new { sp.ProductId, sp.ShoppingCartId });

            modelBuilder.Entity<ShoppingCartProducts>()
                .HasOne(p => p.Product)
                .WithMany(sp => sp.ShoppingCartProducts)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<ShoppingCartProducts>()
                .HasOne(sc => sc.ShoppingCart)
                .WithMany(sp => sp.ShoppingCartProducts)
                .HasForeignKey(s => s.ShoppingCartId);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProducts> ShoppingCartsProducts { get; set; }
        public DbSet<User> AuthorizedUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
