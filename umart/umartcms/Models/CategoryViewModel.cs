﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace umartcms.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string EncId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string Brands { get; set; }
        public string Products { get; set; }
    }
}