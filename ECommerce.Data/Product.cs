using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        
        public string ImageFileName { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<ShoppingCartProducts> ShoppingCartProducts { get; set; }

    }
}
