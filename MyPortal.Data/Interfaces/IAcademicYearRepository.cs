using System.Security.Principal;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IAcademicYearRepository : IReadWriteRepository<AcademicYear>
    {
        Task<AcademicYear> GetCurrent();
    }
}
