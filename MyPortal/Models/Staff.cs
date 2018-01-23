using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models
{
    public class Staff
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsTutor { get; set; }
        public int Count { get; set; }
    }
}