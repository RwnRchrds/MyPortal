using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Search
{
    public class PersonSearch
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
    }
}
