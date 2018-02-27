using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int FourMId { get; set; }
        public string RegGroup { get; set; }
        public string YearGroup { get; set; }
        public int Count { get; set; }
        public decimal AccountBalance { get; set; }
    }
}