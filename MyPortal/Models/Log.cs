using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Author { get; set; }
        public int Student { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}