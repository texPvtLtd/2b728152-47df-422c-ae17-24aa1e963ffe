using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class AddToCart
    {
        public int ID { get; set; }
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
