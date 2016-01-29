using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace umartcms.Models
{
    public class ProductViewModel
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Price { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> Availableitems { get; set; }
        public Nullable<int> ImageId { get; set; }
        public string EncId { get; set; }
        public string AddToCarts { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public string OrderDetails { get; set; }
    }
}