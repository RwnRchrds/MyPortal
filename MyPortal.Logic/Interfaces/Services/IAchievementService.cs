using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAchievementService
    {
        Task<IEnumerable<AchievementModel>> GetAchievementsByStudent(Guid studentId, Guid academicYearId);
        Task<AchievementModel> GetAchievementById(Guid achievementId);
        Task<int> GetAchievementPointsByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetAchievementCountByStudent(Guid studentId, Guid academicYearId);
        Task CreateAchievement(params AchievementModel[] achievements);
        Task UpdateAchievement(params UpdateAchievementModel[] achievements);
        Task DeleteAchievement(params Guid[] achievementIds);
        Task<IEnumerable<AchievementTypeModel>> GetAchievementTypes();
        Task<IEnumerable<AchievementOutcomeModel>> GetAchievementOutcomes();    
    }
}
