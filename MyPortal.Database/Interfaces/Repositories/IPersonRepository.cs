using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Person;
using MyPortal.Database.Models.Search;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IPersonRepository : IReadWriteRepository<Person>, IUpdateRepository<Person>
    {
        Task<Person> GetByUserId(Guid userId);
        Task<IEnumerable<Person>> GetAll(PersonSearchOptions searchParams);
        Task<PersonSearchResult> GetPersonWithTypesById(Guid personId);
        Task<PersonSearchResult> GetPersonWithTypesByUserId(Guid userId);
        Task<IEnumerable<PersonSearchResult>> GetAllWithTypes(PersonSearchOptions searchParams);
        Task<PersonSearchResult> GetPersonWithTypesByDirectoryId(Guid directoryId);
    }
}