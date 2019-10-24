using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class PeopleProcesses
    {
        public static async Task<Person> GetPersonByUserId(string userId, MyPortalDbContext context)
        {
            var person = await context.Persons.SingleOrDefaultAsync(x => x.UserId == userId);

            if (person == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Person not found");
            }

            return person;
        }

        public static async Task<bool> PersonHasDocuments(int personId, MyPortalDbContext context)
        {
            return await context.PersonDocuments.AnyAsync(x => x.PersonId == personId);
        }

        public static async Task UpdatePerson(Person person, MyPortalDbContext context, bool commitImmediately = true)
        {
            var personInDb = await context.Persons.SingleOrDefaultAsync(x => x.Id == person.Id);

            if (personInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Person not found");
            }

            personInDb.Title = person.Title;
            personInDb.FirstName = person.FirstName;
            personInDb.LastName = person.LastName;
            personInDb.Gender = person.Gender;
            personInDb.Dob = person.Dob;
            personInDb.MiddleName = person.MiddleName;
            personInDb.PhotoId = person.PhotoId;
            personInDb.NhsNumber = person.NhsNumber;
            personInDb.Deceased = person.Deceased;

            await context.SaveChangesAsync();
        }

        public static async Task<int> GetNumberOfBirthdaysThisWeek(MyPortalDbContext context)
        {
            var weekBeginning = DateTime.Today.GetDayOfWeek(DayOfWeek.Monday);
            var weekEnd = DateTime.Today.GetDayOfWeek(DayOfWeek.Sunday);

            return await context.Persons.CountAsync(x => x.Dob >= weekBeginning && x.Dob <= weekEnd);
        }

        public static async Task<IEnumerable<Person>> SearchForPerson(Person person, MyPortalDbContext context)
        {
            return await context.Persons.Where(x =>
                (person.FirstName == null || x.FirstName == person.FirstName) &&
                (person.LastName == null || x.LastName == person.LastName) &&
                (person.Dob == null || x.Dob == person.Dob)).ToListAsync();
        }

        public static async Task<IEnumerable<GridMedicalPersonConditionDto>> GetMedicalConditionsByPersonDataGrid(
            int personId, MyPortalDbContext context)
        {
            var conditions = await GetMedicalConditionsByPersonModel(personId, context);

            return conditions.Select(Mapper.Map<MedicalPersonCondition, GridMedicalPersonConditionDto>);
        }

        public static async Task<IEnumerable<MedicalPersonCondition>> GetMedicalConditionsByPersonModel(int personId,
            MyPortalDbContext context)
        {
            var conditions = await context.MedicalPersonConditions.Where(x => x.PersonId == personId).ToListAsync();

            return conditions;
        }

        public static async Task<IEnumerable<GridMedicalPersonDietaryRequirementDto>>
            GetMedicalDietaryRequirementsByPersonDataGrid(int personId, MyPortalDbContext context)
        {
            var dietaryRequirements = await GetMedicalDietaryRequirementsByPersonModel(personId, context);

            return dietaryRequirements.Select(Mapper
                .Map<MedicalPersonDietaryRequirement, GridMedicalPersonDietaryRequirementDto>);
        }

        public static async Task<IEnumerable<MedicalPersonDietaryRequirement>> GetMedicalDietaryRequirementsByPersonModel(
            int personId, MyPortalDbContext context)
        {
            var dietaryRequirements = await context.MedicalPersonDietaryRequirements.Where(x => x.PersonId == personId).ToListAsync();

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