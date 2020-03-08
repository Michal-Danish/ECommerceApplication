using ECommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Models
{
    public class IndividualProductViewModel
    {
        public Product Product { get; set; }
        public int? CartId { get; set; }
    }
}
