using System;
using Syncfusion.EJ2.Inputs;

namespace MyPortal.Logic.Models.Lite
{
    public class PersonLite
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
    }
}