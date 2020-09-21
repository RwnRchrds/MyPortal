using System;

namespace MyPortal.Database.Models.Search
{
    public class PersonSearchOptions
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
    }
}
