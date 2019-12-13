using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IPersonRepository : IReadWriteRepository<Person>
    {
        Task<Person> GetByUserId(string userId);

        Task<IEnumerable<Person>> Search(Person person);

        Task<int> GetNumberOfBirthdaysThisWeek(DateTime weekBeginning);
    }
}
