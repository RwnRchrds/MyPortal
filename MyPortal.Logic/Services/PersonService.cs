using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Requests.Person;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class PersonService : BaseService, IPersonService
    {
        private readonly IPersonRepository _personRepository;

        private Person GenerateSearchObject(PersonSearchParams searchParams)
        {
            var person = new Person();

            person.FirstName = searchParams.FirstName;
            person.MiddleName = searchParams.MiddleName;
            person.LastName = searchParams.LastName;
            person.Gender = searchParams.Gender;
            person.Dob = searchParams.Dob;

            return person;
        }

        public PersonService(IPersonRepository personRepository) : base("Person")
        {
            _personRepository = personRepository;
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


        public async Task<IEnumerable<PersonModel>> Get(PersonSearchParams searchParams)
        {
            var searchObject = GenerateSearchObject(searchParams);

            IEnumerable<Person> people;

            people = await _personRepository.GetAll(searchObject);

            return people.Select(_businessMapper.Map<PersonModel>).ToList();
        }

        public async Task<PersonModel> GetByUserId(Guid userId)
        {
            var person = await _personRepository.GetByUserId(userId);

            if (person == null)
            {
                throw NotFound();
            }

            return _businessMapper.Map<PersonModel>(person);
        }

        public override void Dispose()
        {
            _personRepository.Dispose();
        }
    }
}