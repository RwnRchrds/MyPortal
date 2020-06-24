using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Models;
using MyPortal.Database.Search;

namespace MyPortal.Database.Interfaces
{
    public interface IPersonRepository : IReadWriteRepository<Person>
    {
        Task<Person> GetByUserId(Guid userId);

        Task<IEnumerable<Person>> GetAll(PersonSearch searchParams);

        Task<PersonTypeIndicator> GetPersonTypeIndicatorById(Guid personId);
    }
}
