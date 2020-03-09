using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Details;
using MyPortal.Logic.Models.Person;

namespace MyPortal.Logic.Services
{
    public class PersonService : BaseService, IPersonService
    {
        private readonly IPersonRepository _repository;

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

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
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


        public async Task<IEnumerable<PersonDetails>> Get(PersonSearchParams searchParams)
        {
            var searchObject = GenerateSearchObject(searchParams);

            IEnumerable<Person> people;

            people = await _repository.GetAll(searchObject);

            return people.Select(_businessMapper.Map<PersonDetails>).ToList();
        }
    }
}