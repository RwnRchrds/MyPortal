using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class AchievementService : BaseService, IAchievementService
    {
        private readonly IAcademicYearRepository _academicYearRepository;
        private readonly IAchievementRepository _achievementRepository;
        private readonly IAchievementTypeRepository _achievementTypeRepository;
        private readonly IAchievementOutcomeRepository _achievementOutcomeRepository;

        public AchievementService(ApplicationDbContext context)
        {
            _achievementRepository = new AchievementRepository(context);
            _achievementTypeRepository = new AchievementTypeRepository(context);
            _achievementOutcomeRepository = new AchievementOutcomeRepository(context);
            _academicYearRepository = new AcademicYearRepository(context);
        }

        public override void Dispose()
        {
            _achievementRepository.Dispose();
            _achievementTypeRepository.Dispose();
            _achievementOutcomeRepository.Dispose();
        }

        public async Task<IEnumerable<AchievementModel>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var achievements = await _achievementRepository.GetByStudent(studentId, academicYearId);

            return achievements.Select(BusinessMapper.Map<AchievementModel>).ToList();
        }

        public async Task<AchievementModel> GetById(Guid achievementId)
        {
            var achievement = await _achievementRepository.GetById(achievementId);

            return BusinessMapper.Map<AchievementModel>(achievement);
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

        public async Task CheckYearLock(Guid academicYearId)
        {
            if (await _academicYearRepository.IsLocked(academicYearId))
            {
                throw new InvalidDataException("Academic year is locked and cannot be modified.");
            }
        }

        public async Task Create(params AchievementModel[] achievements)
        {
            foreach (var achievement in achievements)
            {
                await AcademicYearModel.CheckLock(_academicYearRepository, achievement.AcademicYearId);
                
                var model = new Achievement
                {
                    AcademicYearId = achievement.AcademicYearId,
                    AchievementTypeId = achievement.AchievementTypeId,
                    LocationId = achievement.LocationId,
                    StudentId = achievement.StudentId,
                    Comments = achievement.Comments,
                    OutcomeId = achievement.OutcomeId,
                    Points = achievement.Points,
                    RecordedById = achievement.RecordedById,
                    CreatedDate = DateTime.Now
                };

                _achievementRepository.Create(model);
            }

            await _achievementRepository.SaveChanges();
        }

        public async Task Update(params AchievementModel[] achievements)
        {
            foreach (var achievement in achievements)
            {
                await AcademicYearModel.CheckLock(_academicYearRepository, achievement.AcademicYearId);
                
                var achievementInDb = await _achievementRepository.GetByIdWithTracking(achievement.Id);

                if (achievementInDb == null)
                {
                    throw new NotFoundException("Achievement not found.");
                }

                achievementInDb.AchievementTypeId = achievement.AchievementTypeId;
                achievementInDb.LocationId = achievement.LocationId;
                achievementInDb.OutcomeId = achievement.OutcomeId;
                achievementInDb.Comments = achievement.Comments;
                achievementInDb.Points = achievement.Points;
            }

            await _achievementRepository.SaveChanges();
        }

        public async Task Delete(params Guid[] achievementIds)
        {
            foreach (var achievementId in achievementIds)
            {
                var achievement = await GetById(achievementId);

                await AcademicYearModel.CheckLock(_academicYearRepository, achievement.AcademicYearId);
                
                await _achievementRepository.Delete(achievementId);
            }

            await _achievementRepository.SaveChanges();
        }

        public async Task<IEnumerable<AchievementTypeModel>> GetTypes()
        {
            var types = await _achievementTypeRepository.GetAll();

            return types.Select(BusinessMapper.Map<AchievementTypeModel>).ToList();
        }

        public async Task<IEnumerable<AchievementOutcomeModel>> GetOutcomes()
        {
            var outcomes = await _achievementOutcomeRepository.GetAll();

            return outcomes.Select(BusinessMapper.Map<AchievementOutcomeModel>).ToList();
        }
    }
}
