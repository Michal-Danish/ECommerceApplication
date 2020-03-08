using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

        public List<ShoppingCartProducts> ShoppingCartProducts { get; set; }

    }
}
