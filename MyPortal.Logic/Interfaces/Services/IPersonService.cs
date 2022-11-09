using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Response.People;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonModel>> GetPeople(PersonSearchOptions searchModel);
        Task<PersonModel> GetPersonByUserId(Guid userId, bool throwIfNotFound = true);
        Task<PersonModel> GetPersonById(Guid personId);
        Dictionary<string, string> GetGenderOptions();
        Task<PersonSearchResultModel> GetPersonWithTypes(Guid personId);
        Task<PersonSearchResultModel> GetPersonWithTypesByUser(Guid userId);
        Task<PersonSearchResultModel> GetPersonWithTypesByDirectory(Guid directoryId);
        Task<IEnumerable<PersonSearchResultModel>> GetPeopleWithTypes(PersonSearchOptions searchModel);
    }
}
