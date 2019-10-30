using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CommunicationEmailAddressRepository : Repository<CommunicationEmailAddress>, ICommunicationEmailAddressRepository
    {
        public CommunicationEmailAddressRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CommunicationEmailAddress>> GetEmailAddressesByPerson(int personId)
        {
            return await Context.CommunicationEmailAddresses.Where(x => x.PersonId == personId).ToListAsync();
        }
    }
}