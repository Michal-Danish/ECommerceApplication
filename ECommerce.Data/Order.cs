using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerce.Data
{
    public class Order
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string SecurityCode { get; set; }
        public bool? Filled { get; set; }

        [NotMapped]
        public decimal OrderTotal { get; set; }
    }
}
