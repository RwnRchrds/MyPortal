﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Response.People;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonModel>> Get(PersonSearchOptions searchModel);
        Task<PersonModel> GetByUserId(Guid userId, bool throwIfNotFound = true);
        Task<PersonModel> GetById(Guid personId);
        Dictionary<string, string> GetGenderOptions();
        Task<PersonSearchResultResponseModel> GetPersonWithTypes(Guid personId);
        Task<PersonSearchResultResponseModel> GetPersonWithTypesByDirectory(Guid directoryId);
        Task<IEnumerable<PersonSearchResultResponseModel>> GetPeopleWithTypes(PersonSearchOptions searchModel);
    }
}
