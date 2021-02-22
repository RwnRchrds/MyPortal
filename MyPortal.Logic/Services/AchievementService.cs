using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class AchievementService : BaseService, IAchievementService
    {
        public AchievementService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void Dispose()
        {
            UnitOfWork.Dispose();
        }

        public async Task<IEnumerable<AchievementModel>> GetAchievementsByStudent(Guid studentId, Guid academicYearId)
        {
            var achievements = await UnitOfWork.Achievements.GetByStudent(studentId, academicYearId);

            return achievements.Select(BusinessMapper.Map<AchievementModel>).ToList();
        }

        public async Task<AchievementModel> GetAchievementById(Guid achievementId)
        {
            var achievement = await UnitOfWork.Achievements.GetById(achievementId);

            return BusinessMapper.Map<AchievementModel>(achievement);
        }

        public async Task<int> GetAchievementPointsByStudent(Guid studentId, Guid academicYearId)
        {
            var points = await UnitOfWork.Achievements.GetPointsByStudent(studentId, academicYearId);

            return points;
        }

        public async Task<int> GetAchievementCountByStudent(Guid studentId, Guid academicYearId)
        {
            var count = await UnitOfWork.Achievements.GetCountByStudent(studentId, academicYearId);

            return count;
        }

        public async Task CreateAchievement(params AchievementModel[] requests)
        {
            foreach (var request in requests)
            {
                await AcademicYearModel.CheckLock(UnitOfWork.AcademicYears, request.AcademicYearId);
                
                var model = new Achievement
                {
                    AcademicYearId = request.AcademicYearId,
                    AchievementTypeId = request.AchievementTypeId,
                    LocationId = request.LocationId,
                    StudentId = request.StudentId,
                    Comments = request.Comments,
                    OutcomeId = request.OutcomeId,
                    Points = request.Points,
                    RecordedById = request.RecordedById,
                    CreatedDate = DateTime.Now
                };

                UnitOfWork.Achievements.Create(model);
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task UpdateAchievement(params UpdateAchievementModel[] requests)
        {
            foreach (var request in requests)
            {
                var achievementInDb = await UnitOfWork.Achievements.GetByIdForEditing(request.Id);

                if (achievementInDb == null)
                {
                    throw new NotFoundException("Achievement not found.");
                }

                await AcademicYearModel.CheckLock(UnitOfWork.AcademicYears, achievementInDb.AcademicYearId);

                achievementInDb.AchievementTypeId = request.AchievementTypeId;
                achievementInDb.LocationId = request.LocationId;
                achievementInDb.OutcomeId = request.OutcomeId;
                achievementInDb.Comments = request.Comments;
                achievementInDb.Points = request.Points;
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task DeleteAchievement(params Guid[] achievementIds)
        {
            foreach (var achievementId in achievementIds)
            {
                var achievement = await GetAchievementById(achievementId);

                await AcademicYearModel.CheckLock(UnitOfWork.AcademicYears, achievement.AcademicYearId);
                
                await UnitOfWork.Achievements.Delete(achievementId);
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<AchievementTypeModel>> GetAchievementTypes()
        {
            var types = await UnitOfWork.AchievementTypes.GetAll();

            return types.Select(BusinessMapper.Map<AchievementTypeModel>).ToList();
        }

        public async Task<IEnumerable<AchievementOutcomeModel>> GetAchievementOutcomes()
        {
            var outcomes = await UnitOfWork.AchievementOutcomes.GetAll();

            return outcomes.Select(BusinessMapper.Map<AchievementOutcomeModel>).ToList();
        }
    }
}
