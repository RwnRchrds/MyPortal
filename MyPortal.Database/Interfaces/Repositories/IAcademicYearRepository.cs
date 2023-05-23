using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAcademicYearRepository : IReadWriteRepository<AcademicYear>, IUpdateRepository<AcademicYear>
    {
        Task<AcademicYear> GetCurrentAcademicYear();
        Task<AcademicYear> GetLatestAcademicYear();
        Task<AcademicYear> GetAcademicYearByWeek(Guid attendanceWeekId);
        Task<IEnumerable<AcademicYear>> GetAllAcademicYears();
        Task<bool> IsYearLocked(Guid academicYearId);
    }
}
