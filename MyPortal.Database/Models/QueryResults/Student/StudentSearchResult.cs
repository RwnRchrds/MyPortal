using System;

namespace MyPortal.Database.Models.QueryResults.Student
{
    public class StudentSearchResult
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string PreferredFirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PreferredLastName { get; set; }
        public string Gender { get; set; }
        public string HouseName { get; set; }
        public string RegGroupName { get; set; }
        public string YearGroupName { get; set; }
    }
}