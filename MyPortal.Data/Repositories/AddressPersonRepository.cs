using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class AddressPersonRepository : ReadWriteRepository<AddressPerson>, IAddressPersonRepository
    {
        public AddressPersonRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}