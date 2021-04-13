using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAddressRepository : IReadWriteRepository<Address>, IUpdateRepository<Address>
    {
        Task<IEnumerable<Address>> GetAddressesByPerson(Guid personId);
    }
}
