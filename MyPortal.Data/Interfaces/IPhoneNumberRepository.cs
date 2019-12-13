using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IPhoneNumberRepository : IReadWriteRepository<PhoneNumber>
    {
        Task<IEnumerable<PhoneNumber>> GetByPerson(int personId);
    }
}
