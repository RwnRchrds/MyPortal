﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Query;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IPersonService : IService
    {
        Task<IEnumerable<PersonModel>> Get(PersonSearchOptions searchModel);
        Task<PersonModel> GetByUserId(Guid userId, bool throwIfNotFound = true);
        Task<PersonModel> GetById(Guid personId);
        Dictionary<string, string> GetGenderOptions();
        Task<PersonSearchResultModel> GetPersonWithTypes(Guid personId);
        Task<IEnumerable<PersonSearchResultModel>> GetWithTypes(PersonSearchOptions searchModel);
    }
}
