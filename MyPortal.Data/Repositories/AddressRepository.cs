using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class AddressRepository : ReadWriteRepository<Address>, IAddressRepository
    {
        public AddressRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Address>> GetAddressesByPerson(int personId)
        {
            var addresses = await Context.AddressPersons.Where(x => x.PersonId == personId)
                .Select(x => x.Address).ToListAsync();

            return addresses;
        }
    }
}