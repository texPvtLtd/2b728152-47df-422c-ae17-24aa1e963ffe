using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Image
    {
        public Image()
        {
            this.Products = new List<Product>();
        }

        public int ImageId { get; set; }
        public long ProductId { get; set; }
        public string Image1 { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
