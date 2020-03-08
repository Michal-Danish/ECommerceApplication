using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data
{
    public class ShoppingCartProducts
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public int Quantity { get; set; }
    }
}
