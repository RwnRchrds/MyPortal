using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Constants;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;
using MyPortal.Logic.Models.Requests.Behaviour.Detentions;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class BehaviourService : BaseService, IBehaviourService
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
            var user = await GetCurrentUser();
            
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var request in requests)
                {
                    await AcademicHelper.IsAcademicYearLocked(request.AcademicYearId, true);

                    var model = new Achievement
                    {
                        AcademicYearId = request.AcademicYearId,
                        AchievementTypeId = request.AchievementTypeId,
                        LocationId = request.LocationId,
                        StudentId = request.StudentId,
                        Comments = request.Comments,
                        OutcomeId = request.OutcomeId,
                        Points = request.Points,
                        CreatedById = user.Id.Value,
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

                    await AcademicHelper.IsAcademicYearLocked(achievementInDb.AcademicYearId, true);

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

                    await AcademicHelper.IsAcademicYearLocked(achievement.AcademicYearId, true);

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
        
        public async Task<IEnumerable<IncidentModel>> GetIncidentsByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var incidents = await unitOfWork.Incidents.GetByStudent(studentId, academicYearId);

                return incidents.Select(i => new IncidentModel(i));
            }
        }

        public async Task<IncidentModel> GetIncidentById(Guid incidentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var incident = await unitOfWork.Incidents.GetById(incidentId);

                return new IncidentModel(incident);
            }
        }

        public async Task<int> GetBehaviourPointsByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var points = await unitOfWork.Incidents.GetPointsByStudent(studentId, academicYearId);

                return points;
            }
        }

        public async Task<int> GetBehaviourCountByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var count = await unitOfWork.Incidents.GetCountByStudent(studentId, academicYearId);

                return count;
            }
        }

        public async Task CreateIncident(params CreateIncidentModel[] incidents)
        {
            var user = await GetCurrentUser();
            
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var incidentModel in incidents)
                {
                    var incident = new Incident
                    {
                        Points = incidentModel.Points,
                        CreatedDate = DateTime.Now,
                        BehaviourTypeId = incidentModel.BehaviourTypeId,
                        LocationId = incidentModel.LocationId,
                        OutcomeId = incidentModel.OutcomeId,
                        StatusId = incidentModel.StatusId,
                        CreatedById = user.Id.Value,
                        StudentId = incidentModel.StudentId,
                        Comments = incidentModel.Comments,
                        AcademicYearId = incidentModel.AcademicYearId
                    };

                    unitOfWork.Incidents.Create(incident);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateIncident(params UpdateIncidentModel[] incidents)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var incidentModel in incidents)
                {
                    var incidentInDb = await unitOfWork.Incidents.GetById(incidentModel.Id);

                    incidentInDb.Points = incidentModel.Points;
                    incidentInDb.BehaviourTypeId = incidentModel.BehaviourTypeId;
                    incidentInDb.LocationId = incidentModel.LocationId;
                    incidentInDb.OutcomeId = incidentModel.OutcomeId;
                    incidentInDb.StatusId = incidentModel.StatusId;
                    incidentInDb.Comments = incidentModel.Comments;

                    await unitOfWork.Incidents.Update(incidentInDb);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteIncident(params Guid[] incidentIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var incidentId in incidentIds)
                {
                    await unitOfWork.Incidents.Delete(incidentId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<IncidentTypeModel>> GetIncidentTypes()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var types = await unitOfWork.IncidentTypes.GetAll();

                return types.Select(t => new IncidentTypeModel(t)).ToList();
            }
        }

        public async Task<IEnumerable<BehaviourOutcomeModel>> GetIncidentOutcomes()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var outcomes = await unitOfWork.BehaviourOutcomes.GetAll();

                return outcomes.Select(o => new BehaviourOutcomeModel(o)).ToList();
            }
        }

        public async Task<IEnumerable<BehaviourStatusModel>> GetBehaviourStatus()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var status = await unitOfWork.BehaviourStatus.GetAll();

                return status.Select(s => new BehaviourStatusModel(s)).ToList();
            }
        }
        
        public async Task<IEnumerable<DetentionModel>> Get(DetentionSearchOptions searchOptions)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var detentions = await unitOfWork.Detentions.GetAll(searchOptions);

                return detentions.Select(d => new DetentionModel(d));
            }
        }

        public async Task<DetentionModel> GetById(Guid detentionId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var detention = await unitOfWork.Detentions.GetById(detentionId);

                return new DetentionModel(detention);
            }
        }

        public async Task<DetentionModel> GetByIncident(Guid incidentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var detention = await unitOfWork.Detentions.GetByIncident(incidentId);

                return new DetentionModel(detention);
            }
        }

        public async Task Create(params CreateDetentionRequest[] detentionModels)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var model in detentionModels)
                {
                    var detention = new Detention
                    {
                        DetentionTypeId = model.DetentionTypeId,
                        SupervisorId = model.SupervisorId,
                        Event = new DiaryEvent
                        {
                            StartTime = model.StartTime,
                            EndTime = model.EndTime,
                            RoomId = model.RoomId,
                            EventTypeId = EventTypes.Detention,
                            Subject = "Detention"
                        }
                    };

                    unitOfWork.Detentions.Create(detention);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task Update(params UpdateDetentionRequest[] detentionModels)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var model in detentionModels)
                {
                    var detentionInDb = await unitOfWork.Detentions.GetById(model.Id);

                    detentionInDb.DetentionTypeId = model.DetentionTypeId;
                    detentionInDb.Event.StartTime = model.StartTime;
                    detentionInDb.Event.EndTime = model.EndTime;
                    detentionInDb.Event.RoomId = model.RoomId;

                    await unitOfWork.Detentions.Update(detentionInDb);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task Delete(params Guid[] detentionIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var detentionId in detentionIds)
                {
                    await unitOfWork.Detentions.Delete(detentionId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task AddStudent(Guid detentionId, Guid incidentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var incidentDetention = new IncidentDetention
                {
                    DetentionId = detentionId,
                    IncidentId = incidentId
                };

                unitOfWork.IncidentDetentions.Create(incidentDetention);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task RemoveStudent(Guid incidentDetentionId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var relatedIncident = await unitOfWork.IncidentDetentions.GetById(incidentDetentionId);

                if (relatedIncident == null)
                {
                    throw new NotFoundException("Detention not found.");
                }

                await unitOfWork.IncidentDetentions.Delete(relatedIncident.Id);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public BehaviourService(ClaimsPrincipal user) : base(user)
        {
        }
    }
}
