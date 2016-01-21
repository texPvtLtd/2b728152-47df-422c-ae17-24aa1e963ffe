using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Product
    {
        public Product()
        {
            this.AddToCarts = new List<AddToCart>();
            this.OrderDetails = new List<OrderDetail>();
        }

        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Price { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> Availableitems { get; set; }
        public Nullable<int> ImageId { get; set; }
        public string EncId { get; set; }
        public virtual ICollection<AddToCart> AddToCarts { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual Image Image { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
