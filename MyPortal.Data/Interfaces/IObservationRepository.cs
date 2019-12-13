using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IObservationRepository : IReadWriteRepository<Observation>
    {
        Task<IEnumerable<Observation>> GetByStaffMember(int staffId);
    }
}
