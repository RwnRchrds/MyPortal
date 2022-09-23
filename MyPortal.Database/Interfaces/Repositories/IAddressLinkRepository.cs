using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAddressLinkRepository : IReadWriteRepository<AddressLink>, IUpdateRepository<AddressLink>
    {
    }
}
