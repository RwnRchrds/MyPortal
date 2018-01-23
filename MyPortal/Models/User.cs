using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models
{
    public class User
    {
        public string Id { get; set; }
        public string PasswordHash { get; set; }
        public int Type { get; set; }
    }
}