using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.BusinessLogic.Extensions;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class PeopleService : MyPortalService
    {
        public PeopleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public PeopleService() : base()
        {

        }

        public async Task<PersonDto> GetPersonById(int personId)
        {
            var person = await UnitOfWork.People.GetById(personId);

            if (person == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Person not found");
            }

            return Mapping.Map<PersonDto>(person);
        }

        public async Task<PersonDto> GetPersonByUserId(string userId)
        {
            var person = await UnitOfWork.People.GetByUserId(userId);

            if (person == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Person not found");
            }

            return Mapping.Map<PersonDto>(person);
        }

        public async Task UpdatePerson(PersonDto person, bool commitImmediately = true)
        {
            var personInDb = await UnitOfWork.People.GetById(person.Id);

            if (personInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Person not found.");
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

            await UnitOfWork.Complete();
        }

        public async Task<int> GetNumberOfBirthdaysThisWeek()
        {
            var weekBeginning = DateTime.Today.StartOfWeek();

            return await UnitOfWork.People.GetNumberOfBirthdaysThisWeek(weekBeginning);
        }

        public async Task<IEnumerable<PersonDto>> SearchForPerson(PersonDto person)
        {
            return (await UnitOfWork.People.Search(Mapping.Map<Person>(person))).Select(Mapping.Map<PersonDto>);
        }

        public async Task<IEnumerable<PersonConditionDto>> GetMedicalConditionsByPerson(int personId)
        {
            return (await UnitOfWork.PersonConditions.GetByPerson(personId)).Select(Mapping.Map<PersonConditionDto>);
        }

        public async Task<IEnumerable<PersonDietaryRequirementDto>> GetMedicalDietaryRequirementsByPerson(int personId)
        {
            return (await UnitOfWork.PersonDietaryRequirements.GetByPerson(personId)).Select(Mapping.Map<PersonDietaryRequirementDto>);
        }

        public IDictionary<string, string> GetGendersLookup()
        {
            return new Dictionary<string, string>
            {
                { "M", "Male" },
                { "F", "Female" },
                { "X", "Other" },
                { "U", "Unknown" }
            };
        }

        public IEnumerable<string> GetTitles()
        {
            return new List<string>
            {
                "Mr",
                "Mrs",
                "Miss",
                "Ms",
                "Mx",
                "Dr",
                "Sir",
                "Prof",
                "Rev",
                "Lady",
                "Lord"
            };
        }
    }
}