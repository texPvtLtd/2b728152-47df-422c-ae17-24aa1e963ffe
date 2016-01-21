using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string EncId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
