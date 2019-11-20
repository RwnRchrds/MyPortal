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
    public class CommunicationAddressRepository : Repository<CommunicationAddress>, ICommunicationAddressRepository
    {
        public CommunicationAddressRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CommunicationAddress>> GetAddressesByPerson(int personId)
        {
            var addresses = await Context.CommunicationAddressPersons.Where(x => x.PersonId == personId)
                .Select(x => x.Address).ToListAsync();

            return addresses;
        }
    }
}