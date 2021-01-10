using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Query;
using MyPortal.Database.Models.Query.Person;
using MyPortal.Database.Models.Search;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IPersonRepository : IReadWriteRepository<Person>
    {
        Task<Person> GetByUserId(Guid userId);
        Task<IEnumerable<Person>> GetAll(PersonSearchOptions searchParams);
        Task<PersonSearchResult> GetPersonWithTypesById(Guid personId);
        Task<IEnumerable<PersonSearchResult>> GetAllWithTypes(PersonSearchOptions searchParams);
    }
}
