using System;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Requests.Person;

namespace MyPortal.Logic.Helpers
{
    public static class PersonHelper
    {
        public static Person CreatePerson(CreatePersonModel model)
        {
            var createDate = DateTime.Now;

            return new Person
            {
                FirstName = model.FirstName,
                PreferredFirstName = model.PreferredFirstName,
                LastName = model.PreferredLastName,
                PreferredLastName = model.PreferredLastName,
                MiddleName = model.MiddleName,
                Title = model.Title,
                NhsNumber = model.NhsNumber,
                CreatedDate = createDate,
                Deleted = false,
                Gender = model.Gender,
                Dob = model.Dob,
                EthnicityId = model.EthnicityId,
                Directory = new Directory
                {
                    Name = "person-root"
                }
            };
        }

        public static void UpdatePerson(Person person, UpdatePersonModel model)
        {
            person.FirstName = model.FirstName;
            person.PreferredFirstName = model.PreferredFirstName;
            person.LastName = model.LastName;
            person.PreferredLastName = model.PreferredLastName;
            person.MiddleName = model.MiddleName;
            person.Title = model.Title;
            person.NhsNumber = model.NhsNumber;
            person.Gender = model.Gender;
            person.Dob = model.Dob;
            person.EthnicityId = model.EthnicityId;
            person.Deceased = model.Deceased;
            person.PhotoId = model.PhotoId;
        }
    }
}