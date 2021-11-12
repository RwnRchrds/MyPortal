using System;
using MyPortal.Database.Models.Query.Student;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Collection
{
    public class StudentCollectionModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string HouseName { get; set; }
        public string RegGroupName { get; set; }
        public string YearGroupName { get; set; }

        public StudentCollectionModel(StudentSearchResult searchResult)
        {
            Id = searchResult.Id;
            FirstName = string.IsNullOrWhiteSpace(searchResult.PreferredFirstName)
                ? searchResult.FirstName
                : searchResult.PreferredFirstName;
            LastName = string.IsNullOrWhiteSpace(searchResult.PreferredLastName)
                ? searchResult.LastName
                : searchResult.PreferredLastName;
            Gender = Constants.Gender.GetGenderLabel(searchResult.Gender);
            HouseName = searchResult.HouseName;
            RegGroupName = searchResult.RegGroupName;
            YearGroupName = searchResult.YearGroupName;
        }
    }
}