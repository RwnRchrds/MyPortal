using System;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Collection
{
    public class StudentCollectionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string HouseName { get; set; }
        public string RegGroupName { get; set; }
        public string YearGroupName { get; set; }

        public StudentCollectionModel(StudentModel model)
        {
            Name = model.Person.GetName();
            Gender = Database.Models.Entity.Gender.GenderLabels[model.Person.Gender];
        }
    }
}