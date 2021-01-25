using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Response.People;

namespace MyPortal.Logic.Services
{
    public class PersonService : BaseService, IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonModel> GetById(Guid personId)
        {
            var person = await _personRepository.GetById(personId);

            return BusinessMapper.Map<PersonModel>(person);
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
            var people = await _personRepository.GetAll(searchModel);

            return people.Select(BusinessMapper.Map<PersonModel>).ToList();
        }

        public async Task<IEnumerable<PersonSearchResultModel>> GetWithTypes(PersonSearchOptions searchModel)
        {
            var results = await _personRepository.GetAllWithTypes(searchModel);

            return results.Select(BusinessMapper.Map<PersonSearchResultModel>);
        }

        public async Task<PersonSearchResultModel> GetPersonWithTypes(Guid personId)
        {
            var result = await _personRepository.GetPersonWithTypesById(personId);

            return BusinessMapper.Map<PersonSearchResultModel>(result);
        }

        public async Task<PersonModel> GetByUserId(Guid userId, bool throwIfNotFound = true)
        {
            var person = await _personRepository.GetByUserId(userId);

            if (person == null && throwIfNotFound)
            {
                throw new NotFoundException("Person not found.");
            }

            return BusinessMapper.Map<PersonModel>(person);
        }

        public override void Dispose()
        {
            _personRepository.Dispose();
        }
    }
}