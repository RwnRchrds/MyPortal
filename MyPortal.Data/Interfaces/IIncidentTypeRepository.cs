using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IIncidentTypeRepository : IReadRepository<IncidentType>
    {
        Task<IEnumerable<IncidentType>> GetRecorded(int academicYearId);
    }
}
