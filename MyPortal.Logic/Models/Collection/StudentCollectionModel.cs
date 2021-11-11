using System;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Collection
{
    public class StudentCollectionModel
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string HouseName { get; set; }
        public string RegGroupName { get; set; }
        public string YearGroupName { get; set; }

        public StudentCollectionModel(StudentModel model)
        {
            Firstname = string.IsNullOrWhiteSpace(model.Person.PreferredFirstName)
                ? model.Person.FirstName
                : model.Person.PreferredFirstName;
            Lastname = string.IsNullOrWhiteSpace(model.Person.PreferredLastName)
                ? model.Person.LastName
                : model.Person.PreferredLastName;
            Gender = Database.Models.Entity.Gender.GenderLabels[model.Person.Gender];
        }
    }
}