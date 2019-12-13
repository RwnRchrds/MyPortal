using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class PersonRepository : ReadWriteRepository<Person>, IPersonRepository
    {
        public PersonRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<int> GetNumberOfBirthdaysThisWeek(DateTime weekBeginning)
        {
            var monday = weekBeginning;
            var sunday = weekBeginning.AddDays(6);

            return await Context.People.CountAsync(x => x.Dob >= monday && x.Dob <= sunday);
        }

        public async Task<Person> GetByUserId(string userId)
        {
            return await Context.People.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<IEnumerable<Person>> Search(Person person)
        {
            return await Context.People
                .Where(x => (person.LastName == null || x.LastName == person.LastName) &&
                            (person.FirstName == null || x.FirstName == person.FirstName) &&
                            (person.Dob == null || x.Dob == person.Dob)).ToListAsync();
        }
    }
}