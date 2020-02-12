using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces
{
    public interface IAddressRepository : IReadWriteRepository<Address>
    {
        Task<IEnumerable<Address>> GetAddressesByPerson(int personId);
    }
}
