using ECommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Models
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string CategoryName { get; set; }
    }
}
