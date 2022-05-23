using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Models.QueryResults.Person;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Response.People;

namespace MyPortal.Logic.Services
{
    public class PersonService : BaseService, IPersonService
    {
        public async Task<PersonModel> GetById(Guid personId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var person = await unitOfWork.People.GetById(personId);

                return new PersonModel(person);
            }
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

        public async Task<IEnumerable<PersonModel>> Get(PersonSearchOptions searchModel)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var people = await unitOfWork.People.GetAll(searchModel);

                return people.Select(p => new PersonModel(p)).ToList();
            }
        }

        public async Task<IEnumerable<PersonSearchResultResponseModel>> GetPeopleWithTypes(PersonSearchOptions searchModel)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var results = await unitOfWork.People.GetAllWithTypes(searchModel);

                return results.Select(r => new PersonSearchResultResponseModel(r)).ToList();
            }
        }

        public async Task<PersonSearchResultResponseModel> GetPersonWithTypes(Guid personId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var result = await unitOfWork.People.GetPersonWithTypesById(personId);

                return new PersonSearchResultResponseModel(result);
            }
        }

        public async Task<PersonSearchResultResponseModel> GetPersonWithTypesByDirectory(Guid directoryId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var result = await unitOfWork.People.GetPersonWithTypesByDirectoryId(directoryId);

                return new PersonSearchResultResponseModel(result);
            }
        }

        public async Task<PersonModel> GetByUserId(Guid userId, bool throwIfNotFound = true)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var person = await unitOfWork.People.GetByUserId(userId);

                if (person == null && throwIfNotFound)
                {
                    throw new NotFoundException("Person not found.");
                }

                return new PersonModel(person);
            }
        }
    }
}