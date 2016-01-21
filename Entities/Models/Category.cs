using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Category
    {
        public Category()
        {
            this.Brands = new List<Brand>();
            this.Products = new List<Product>();
        }

        public int CategoryId { get; set; }
        public string EncId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
