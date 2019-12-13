using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class EmailAddressRepository : ReadWriteRepository<EmailAddress>, IEmailAddressRepository
    {
        public EmailAddressRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<EmailAddress>> GetByPerson(int personId)
        {
            return await Context.EmailAddresses.Where(x => x.PersonId == personId).ToListAsync();
        }
    }
}