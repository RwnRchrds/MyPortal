using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IBehaviourAchievementRepository : IReadWriteRepository<BehaviourAchievement>
    {
        Task<int> GetCountByStudent(int studentId, int academicYearId);
        Task<int> GetPointsByStudent(int studentId, int academicYearId);
        Task<IEnumerable<BehaviourAchievement>> GetByStudent(int studentId, int academicYearId);
        Task<int> GetPointsToday();
    }
}
