using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAddressPersonRepository : IReadWriteRepository<AddressPerson>, IUpdateRepository<AddressPerson>
    {
        Task<IEnumerable<AddressPerson>> GetByPerson(Guid personId);
        Task<IEnumerable<AddressPerson>> GetByAddress(Guid addressId);
    }
}