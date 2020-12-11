using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IAchievementService : IService
    {
        Task<IEnumerable<AchievementModel>> GetByStudent(Guid studentId, Guid academicYearId);
        Task<AchievementModel> GetById(Guid achievementId);
        Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetCountByStudent(Guid studentId, Guid academicYearId);
        Task Create(params AchievementModel[] achievements);
        Task Update(params AchievementModel[] achievements);
        Task Delete(params Guid[] achievementIds);
        Task<IEnumerable<AchievementTypeModel>> GetTypes();
        Task<IEnumerable<AchievementOutcomeModel>> GetOutcomes();
    }
}
