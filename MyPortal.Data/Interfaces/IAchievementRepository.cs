using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IAchievementRepository : IReadWriteRepository<Achievement>
    {
        Task<int> GetCountByStudent(int studentId, int academicYearId);
        Task<int> GetPointsByStudent(int studentId, int academicYearId);
        Task<IEnumerable<Achievement>> GetByStudent(int studentId, int academicYearId);
        Task<int> GetPointsToday();
    }
}
