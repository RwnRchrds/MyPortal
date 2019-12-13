using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IClassRepository : IReadWriteRepository<Class>
    {
        Task<IEnumerable<Class>> GetByAcademicYear(int academicYearId);

        Task<IEnumerable<Class>> GetBySubject(int subjectId, int academicYearId);
    }
}
