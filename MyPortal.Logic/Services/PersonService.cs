﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Dtos;
using MyPortal.Logic.Models.Lite;

namespace MyPortal.Logic.Services
{
    public class PersonService : BaseService, IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PersonLite>> Search(PersonLite person)
        {
            var people = await _repository.Search(_businessMapper.Map<Person>(person));

            return people.Select(_businessMapper.Map<PersonLite>);
        }
    }
}