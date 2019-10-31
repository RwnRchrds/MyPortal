using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> GetPersonByUserId(string userId);

        Task<IEnumerable<Person>> SearchPeople(Person person);
    }
}
