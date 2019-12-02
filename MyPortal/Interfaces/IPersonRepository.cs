using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IPersonRepository : IReadWriteRepository<Person>
    {
        Task<Person> GetByUserId(string userId);

        Task<IEnumerable<Person>> Search(Person person);

        Task<int> GetNumberOfBirthdaysThisWeek();
    }
}
