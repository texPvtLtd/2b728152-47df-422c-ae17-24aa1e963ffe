using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Brand
    {
        public Brand()
        {
            this.Products = new List<Product>();
        }

        public int BrandId { get; set; }
        public string EncId { get; set; }
        public string BrandName { get; set; }
        public string BrandCode { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string BrandIcon { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
