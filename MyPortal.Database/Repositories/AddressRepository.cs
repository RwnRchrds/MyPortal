using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class AddressRepository : BaseReadWriteRepository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
