using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAcademicYearRepository : IReadWriteRepository<AcademicYear>
    {
        Task<AcademicYear> GetCurrent();
        Task<IEnumerable<AcademicYear>> GetAllToDate();
    }
}
