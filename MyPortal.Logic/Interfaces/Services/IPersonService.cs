using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Data.People;
using MyPortal.Logic.Models.Requests.Person;


namespace MyPortal.Logic.Interfaces.Services
{
    public interface IPersonService : IService
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
