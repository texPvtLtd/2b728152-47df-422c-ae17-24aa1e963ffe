using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace umartcms.Models
{
    public class BrandViewModel
    {
        public int BrandId { get; set; }
        public string EncId { get; set; }
        public string BrandName { get; set; }
        public string BrandCode { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string BrandIcon { get; set; }
       // public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }



       // public virtual Category Category { get; set; }
      //  public virtual ICollection<Product> Products { get; set; }
    }
}