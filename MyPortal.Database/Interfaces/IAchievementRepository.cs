using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces
{
    public interface IAchievementRepository : IReadWriteRepository<Achievement>
    {
        Task<int> GetCountByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId);
        Task<IEnumerable<Achievement>> GetByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetPointsToday();
    }
}
