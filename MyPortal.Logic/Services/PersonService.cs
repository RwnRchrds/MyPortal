using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Search;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Requests.Person;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class PersonService : BaseService, IPersonService
    {
        private readonly IPersonRepository _personRepository;

        private PersonSearchOptions GenerateSearchObject(PersonSearchOptions searchModel)
        {
            var person = new PersonSearchOptions();

            person.FirstName = searchModel.FirstName;
            //person.MiddleName = searchModel.MiddleName;
            person.LastName = searchModel.LastName;
            person.Gender = searchModel.Gender;
            person.Dob = searchModel.Dob;

            return person;
        }

        public PersonService(IPersonRepository personRepository) : base("Person")
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
            var searchObject = GenerateSearchObject(searchModel);

            IEnumerable<Person> people;

            people = await _personRepository.GetAll(searchObject);

            return people.Select(BusinessMapper.Map<PersonModel>).ToList();
        }

        public async Task<PersonTypeIndicator> GetPersonTypes(Guid personId)
        {
            var types = await _personRepository.GetPersonTypeIndicatorById(personId);

            return types;
        }

        public async Task<PersonModel> GetByUserId(Guid userId)
        {
            var person = await _personRepository.GetByUserId(userId);

            if (person == null)
            {
                throw NotFound();
            }

            return BusinessMapper.Map<PersonModel>(person);
        }

        public override void Dispose()
        {
            _personRepository.Dispose();
        }
    }
}