using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

                return BusinessMapper.Map<PersonModel>(person);
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

                return people.Select(BusinessMapper.Map<PersonModel>).ToList();
            }
        }

        public async Task<IEnumerable<PersonSearchResultModel>> GetWithTypes(PersonSearchOptions searchModel)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var results = await unitOfWork.People.GetAllWithTypes(searchModel);

                return results.Select(BusinessMapper.Map<PersonSearchResultModel>);
            }
        }

        public async Task<PersonSearchResultModel> GetPersonWithTypes(Guid personId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var result = await unitOfWork.People.GetPersonWithTypesById(personId);

                return BusinessMapper.Map<PersonSearchResultModel>(result);
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

                return BusinessMapper.Map<PersonModel>(person);
            }
        }
    }
}