using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos.GridDtos;
using MyPortal.Exceptions;
using MyPortal.Extensions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Services
{
    public class PeopleService : MyPortalService
    {
        public PeopleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<Person> GetPersonById(int personId)
        {
            var person = await UnitOfWork.People.GetByIdAsync(personId);

            if (person == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Person not found");
            }

            return person;
        }

        public async Task<Person> GetPersonByUserId(string userId)
        {
            var person = await UnitOfWork.People.GetPersonByUserId(userId);

            if (person == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Person not found");
            }

            return person;
        }

        public async Task UpdatePerson(Person person, bool commitImmediately = true)
        {
            var personInDb = await GetPersonById(person.Id);

            personInDb.Title = person.Title;
            personInDb.FirstName = person.FirstName;
            personInDb.LastName = person.LastName;
            personInDb.Gender = person.Gender;
            personInDb.Dob = person.Dob;
            personInDb.MiddleName = person.MiddleName;
            personInDb.PhotoId = person.PhotoId;
            personInDb.NhsNumber = person.NhsNumber;
            personInDb.Deceased = person.Deceased;

            await UnitOfWork.Complete();
        }

        public async Task<int> GetNumberOfBirthdaysThisWeek()
        {
            var weekBeginning = DateTime.Today.StartOfWeek();
            var weekEnd = DateTime.Today.GetDayOfWeek(DayOfWeek.Sunday);

            return await UnitOfWork.People.GetNumberOfBirthdaysThisWeek();
        }

        public async Task<IEnumerable<Person>> SearchForPerson(Person person)
        {
            return await UnitOfWork.People.SearchPeople(person);
        }

        public async Task<IEnumerable<MedicalPersonCondition>> GetMedicalConditionsByPerson(int personId)
        {
            var conditions = await UnitOfWork.MedicalPersonConditions.GetMedicalConditionsByPerson(personId);

            return conditions;
        }

        public async Task<IEnumerable<MedicalPersonDietaryRequirement>> GetMedicalDietaryRequirementsByPerson(int personId)
        {
            var dietaryRequirements =
                await UnitOfWork.MedicalPersonDietaryRequirements.GetDietaryRequirementsByPerson(personId);

            return dietaryRequirements;
        }

        public static string GetGenderDisplayName(string genderCode)
        {
            switch (genderCode)
            {
                case "M":
                    return "Male";
                case "F":
                    return "Female";
                case "X":
                    return "Other";
                default:
                    return "Unknown";
            }
        }

        public static IDictionary<string, string> GetGenderLookup()
        {
            return new Dictionary<string, string>
            {
                { "M", "Male" },
                { "F", "Female" },
                { "X", "Other" },
                { "U", "Unknown" }
            };
        }
    }
}