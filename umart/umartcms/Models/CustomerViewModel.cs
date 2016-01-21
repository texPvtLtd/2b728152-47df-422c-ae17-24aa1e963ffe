using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace umartcms.Models
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string EmaiId { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string CustomerName { get; set; }
        public string Encid { get; set; }
    }
}