using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace umartcms.Models
{
    public class OrderViewModel
    {
        public long OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string Contact { get; set; }
        public long ProductId { get; set; }
        public int OrderQuantity { get; set; }
        public string DeliveryDate { get; set; }
        public string  Product { get; set; }
    }
}