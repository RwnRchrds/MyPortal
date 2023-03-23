using System;
using MyPortal.Database.Models.QueryResults.Student;

namespace MyPortal.Logic.Models.Summary
{
    public class StudentSummaryModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string HouseName { get; set; }
        public string RegGroupName { get; set; }
        public string YearGroupName { get; set; }

        public StudentSummaryModel(StudentSearchResult searchResult)
        {
            Id = searchResult.Id;
            FirstName = string.IsNullOrWhiteSpace(searchResult.PreferredFirstName)
                ? searchResult.FirstName
                : searchResult.PreferredFirstName;
            LastName = string.IsNullOrWhiteSpace(searchResult.PreferredLastName)
                ? searchResult.LastName
                : searchResult.PreferredLastName;
            Gender = Constants.Sexes.GetGenderLabel(searchResult.Gender);
            HouseName = searchResult.HouseName;
            RegGroupName = searchResult.RegGroupName;
            YearGroupName = searchResult.YearGroupName;
        }
    }
}