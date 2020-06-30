using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Services
{
    public class AchievementService : BaseService, IAchievementService
    {
        private readonly IAchievementRepository _achievementRepository;
        
        public AchievementService(IAchievementRepository achievementRepository) : base("Achievement")
        {
            _achievementRepository = achievementRepository;
        }

        public override void Dispose()
        {
            _achievementRepository.Dispose();
        }

        public async Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId)
        {
            var points = await _achievementRepository.GetPointsByStudent(studentId, academicYearId);

            return points;
        }

        public async Task<int> GetCountByStudent(Guid studentId, Guid academicYearId)
        {
            var count = await _achievementRepository.GetCountByStudent(studentId, academicYearId);

            return count;
        }

        public async Task Create(params AchievementModel[] achievements)
        {
            foreach (var achievement in achievements)
            {

            }
        }

        public async Task Update(params AchievementModel[] achievements)
        {
            foreach (var achievement in achievements)
            {
                
            }
        }

        public async Task Delete(params Guid[] achievementIds)
        {
            foreach (var achievementId in achievementIds)
            {
                
            }
        }
    }
}
