using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Person;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.People;
using MyPortal.Logic.Models.Requests.Person;


namespace MyPortal.Logic.Services
{
    public class PersonService : BaseService, IPersonService
    {
        public PersonService(ISessionUser user) : base(user)
        {
        }

        public async Task<PersonModel> GetPersonById(Guid personId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var person = await unitOfWork.People.GetById(personId);

            return new PersonModel(person);
        }

        public Dictionary<string, string> GetGenderOptions()
        {
            var genders = new Dictionary<string, string>();

            genders.Add("Male", "M");
            genders.Add("Female", "F");
            genders.Add("Other", "X");
            genders.Add("Unknown", "U");

            return genders;
        }

        public async Task<IEnumerable<PersonModel>> GetPeople(PersonSearchOptions searchModel)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var people = await unitOfWork.People.GetAll(searchModel);

            return people.Select(p => new PersonModel(p)).ToList();
        }

        public async Task<IEnumerable<PersonSearchResultModel>> GetPeopleWithTypes(PersonSearchOptions searchModel)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var results = await unitOfWork.People.GetAllWithTypes(searchModel);

            return results.Select(r => new PersonSearchResultModel(r)).ToList();
        }

        public async Task<PersonSearchResultModel> GetPersonWithTypesById(Guid personId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var result = await unitOfWork.People.GetPersonWithTypesById(personId);

            if (result == null)
            {
                throw new NotFoundException("Person not found.");
            }

            return new PersonSearchResultModel(result);
        }

        public async Task<PersonSearchResultModel> GetPersonWithTypesByUser(Guid userId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var result = await unitOfWork.People.GetPersonWithTypesByUserId(userId);

            return new PersonSearchResultModel(result);
        }

        public async Task<PersonSearchResultModel> GetPersonWithTypesByDirectory(Guid directoryId)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var result = await unitOfWork.People.GetPersonWithTypesByDirectoryId(directoryId);

            return new PersonSearchResultModel(result);
        }

        public async Task<PersonModel> GetPersonByUserId(Guid userId, bool throwIfNotFound = true)
        {
            await using var unitOfWork = await User.GetConnection();
            
            var person = await unitOfWork.People.GetByUserId(userId);

            if (person == null && throwIfNotFound)
            {
                throw new NotFoundException("Person not found.");
            }

            return person != null ? new PersonModel(person) : null;
        }
    }
}