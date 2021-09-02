using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class AchievementService : BaseService, IAchievementService
    {
        public async Task<IEnumerable<AchievementModel>> GetAchievementsByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var achievements = await unitOfWork.Achievements.GetByStudent(studentId, academicYearId);

                return achievements.Select(a => new AchievementModel(a)).ToList();
            }
        }

        public async Task<AchievementModel> GetAchievementById(Guid achievementId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var achievement = await unitOfWork.Achievements.GetById(achievementId);

                return new AchievementModel(achievement);
            }
        }

        public async Task<int> GetAchievementPointsByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var points = await unitOfWork.Achievements.GetPointsByStudent(studentId, academicYearId);

                return points;
            }
        }

        public async Task<int> GetAchievementCountByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var count = await unitOfWork.Achievements.GetCountByStudent(studentId, academicYearId);

                return count;
            }
        }

        public async Task CreateAchievement(params CreateAchievementModel[] requests)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var request in requests)
                {
                    await AcademicYearModel.CheckLock(unitOfWork, request.AcademicYearId);

                    var model = new Achievement
                    {
                        AcademicYearId = request.AcademicYearId,
                        AchievementTypeId = request.AchievementTypeId,
                        LocationId = request.LocationId,
                        StudentId = request.StudentId,
                        Comments = request.Comments,
                        OutcomeId = request.OutcomeId,
                        Points = request.Points,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.Now
                    };

                    unitOfWork.Achievements.Create(model);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateAchievement(params UpdateAchievementModel[] requests)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var request in requests)
                {
                    var achievementInDb = await unitOfWork.Achievements.GetById(request.Id);

                    if (achievementInDb == null)
                    {
                        throw new NotFoundException("Achievement not found.");
                    }

                    await AcademicYearModel.CheckLock(unitOfWork, achievementInDb.AcademicYearId);

                    achievementInDb.AchievementTypeId = request.AchievementTypeId;
                    achievementInDb.LocationId = request.LocationId;
                    achievementInDb.OutcomeId = request.OutcomeId;
                    achievementInDb.Comments = request.Comments;
                    achievementInDb.Points = request.Points;

                    await unitOfWork.Achievements.Update(achievementInDb);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteAchievement(params Guid[] achievementIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var achievementId in achievementIds)
                {
                    var achievement = await GetAchievementById(achievementId);

                    await AcademicYearModel.CheckLock(unitOfWork, achievement.AcademicYearId);

                    await unitOfWork.Achievements.Delete(achievementId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AchievementTypeModel>> GetAchievementTypes()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var types = await unitOfWork.AchievementTypes.GetAll();

                return types.Select(t => new AchievementTypeModel(t)).ToList();
            }
        }

        public async Task<IEnumerable<AchievementOutcomeModel>> GetAchievementOutcomes()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var outcomes = await unitOfWork.AchievementOutcomes.GetAll();

                return outcomes.Select(o => new AchievementOutcomeModel(o)).ToList();
            }
        }
    }
}
