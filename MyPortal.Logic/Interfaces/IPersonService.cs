using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Person;

namespace MyPortal.Logic.Interfaces
{
    public interface IPersonService : IService
    {
        Task<IEnumerable<PersonModel>> Get(PersonSearchOptions searchModel);
        Task<PersonModel> GetByUserId(Guid userId, bool throwIfNotFound = true);
        Task<PersonModel> GetById(Guid personId);
        Dictionary<string, string> GetGenderOptions();
        Task<PersonTypeIndicator> GetPersonTypes(Guid personId);
    }
}
