using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAchievementRepository : IReadWriteRepository<Achievement>
    {
        Task<int> GetCountByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId);
        Task<IEnumerable<Achievement>> GetByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetPointsToday();
    }
}
