using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class PhoneNumberRepository : ReadWriteRepository<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<PhoneNumber>> GetByPerson(int personId)
        {
            return await Context.PhoneNumbers.Where(x => x.PersonId == personId).ToListAsync();
        }
    }
}