using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces
{
    public interface IPersonRepository : IReadWriteRepository<Person>
    {
        Task<Person> GetByUserId(Guid userId);

        Task<IEnumerable<Person>> GetAll(Person person);

        Task<int> GetNumberOfBirthdaysThisWeek(DateTime weekBeginning);
    }
}
