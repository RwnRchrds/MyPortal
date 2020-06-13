using System;

namespace MyPortal.Logic.Models.Requests.Person
{
    public class PersonSearchModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
    }
}