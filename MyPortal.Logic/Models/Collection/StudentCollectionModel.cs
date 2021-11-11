using System;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Collection
{
    public class StudentCollectionModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string PreferredFirstName { get; set; }
        public string LastName { get; set; }
        public string PreferredLastName { get; set; }
        public string Gender { get; set; }
        public string HouseName { get; set; }
        public string RegGroupName { get; set; }
        public string YearGroupName { get; set; }
    }
}