using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IBehaviourAchievementRepository : IRepository<BehaviourAchievement>
    {
        Task<int> GetAchievementCountByStudent(int studentId, int academicYearId);
        Task<int> GetAchievementPointsCountByStudent(int studentId, int academicYearId);
        Task<IEnumerable<BehaviourAchievement>> GetAchievementsByStudent(int studentId, int academicYearId);
        Task<int> GetBehaviourAchievementPointsToday();
    }
}
