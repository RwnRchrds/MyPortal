using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Person
{
    public class CreatePersonModel
    {
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string ChosenFirstName { get; set; }

        public string NhsNumber { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Gender { get; set; }

        public DateTime? Dob { get; set; }

        public DateTime? Deceased { get; set; }

        public Guid? EthnicityId { get; set; }
    }
}
