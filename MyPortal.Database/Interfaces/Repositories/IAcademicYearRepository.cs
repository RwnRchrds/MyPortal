using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAcademicYearRepository : IReadWriteRepository<AcademicYear>, IUpdateRepository<AcademicYear>
    {
        Task<AcademicYear> GetCurrent();
        Task<AcademicYear> GetLatest();
        Task<IEnumerable<AcademicYear>> GetAllToDate();
        Task<bool> IsLocked(Guid academicYearId);
    }
}
