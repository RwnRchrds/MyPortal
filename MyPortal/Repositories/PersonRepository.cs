using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Extensions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<int> GetNumberOfBirthdaysThisWeek()
        {
            var monday = DateTime.Today.StartOfWeek();
            var sunday = DateTime.Today.GetDayOfWeek(DayOfWeek.Sunday);

            return await Context.Persons.CountAsync(x => x.Dob >= monday && x.Dob <= sunday);
        }

        public async Task<Person> GetPersonByUserId(string userId)
        {
            return await Context.Persons.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<IEnumerable<Person>> SearchPeople(Person person)
        {
            return await Context.Persons
                .Where(x => (person.LastName == null || x.LastName == person.LastName) &&
                            (person.FirstName == null || x.FirstName == person.FirstName) &&
                            (person.Dob == null || x.Dob == person.Dob)).ToListAsync();
        }
    }
}