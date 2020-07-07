using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces
{
    public interface IAchievementService : IService
    {
        Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetCountByStudent(Guid studentId, Guid academicYearId);
        Task Create(params AchievementModel[] achievements);
        Task Update(params AchievementModel[] achievements);
        Task Delete(params Guid[] achievementIds);
    }
}
