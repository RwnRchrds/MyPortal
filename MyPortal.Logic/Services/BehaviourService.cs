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
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;
using MyPortal.Logic.Models.Requests.Behaviour.Detentions;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class BehaviourService : BaseService, IBehaviourService
    {
        public async Task<IEnumerable<StudentAchievementSummaryModel>> GetAchievementsByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var achievements =
                    (await unitOfWork.StudentAchievements.GetByStudent(studentId, academicYearId)).Select(a =>
                        new StudentAchievementModel(a));

                var summaries = new List<StudentAchievementSummaryModel>();

                foreach (var achievementModel in achievements)
                {
                    summaries.Add(await StudentAchievementSummaryModel.GetSummary(unitOfWork, achievementModel));
                }

                return summaries;
            }
        }

        public async Task<StudentAchievementModel> GetAchievementById(Guid achievementId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var achievement = await unitOfWork.StudentAchievements.GetById(achievementId);

                return new StudentAchievementModel(achievement);
            }
        }

        public async Task<int> GetAchievementPointsByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var points = await unitOfWork.StudentAchievements.GetPointsByStudent(studentId, academicYearId);

                return points;
            }
        }

        public async Task<int> GetAchievementCountByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var count = await unitOfWork.StudentAchievements.GetCountByStudent(studentId, academicYearId);

                return count;
            }
        }

        public async Task CreateAchievement(AchievementRequestModel achievement)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var now = DateTime.Now;
                
                await AcademicHelper.IsAcademicYearLocked(achievement.AcademicYearId, true);

                var model = new StudentAchievement
                {
                    StudentId = achievement.StudentId,
                    OutcomeId = achievement.OutcomeId,
                    Points = achievement.Points,
                    Achievement = new Achievement
                    {
                        AcademicYearId = achievement.AcademicYearId,
                        AchievementTypeId = achievement.AchievementTypeId,
                        LocationId = achievement.LocationId,
                        Comments = achievement.Comments,
                        CreatedById = achievement.CreatedById,
                        CreatedDate = now
                    }
                };

                unitOfWork.StudentAchievements.Create(model);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateAchievement(Guid achievementId, AchievementRequestModel achievement)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var achievementInDb = await unitOfWork.StudentAchievements.GetById(achievementId);

                if (achievementInDb == null)
                {
                    throw new NotFoundException("Achievement not found.");
                }

                await AcademicHelper.IsAcademicYearLocked(achievementInDb.Achievement.AcademicYearId, true);

                achievementInDb.Achievement.AchievementTypeId = achievement.AchievementTypeId;
                achievementInDb.Achievement.LocationId = achievement.LocationId;
                achievementInDb.OutcomeId = achievement.OutcomeId;
                achievementInDb.Achievement.Comments = achievement.Comments;
                achievementInDb.Points = achievement.Points;

                await unitOfWork.StudentAchievements.Update(achievementInDb);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteAchievement(Guid achievementId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var achievement = await GetAchievementById(achievementId);

                await AcademicHelper.IsAcademicYearLocked(achievement.Achievement.AcademicYearId, true);

                await unitOfWork.Achievements.Delete(achievementId);

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
        
        public async Task<IEnumerable<StudentIncidentSummaryModel>> GetIncidentsByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var incidents = await unitOfWork.StudentIncidents.GetByStudent(studentId, academicYearId);

                var models = incidents.Select(i => new StudentIncidentModel(i));

                var results = new List<StudentIncidentSummaryModel>();

                foreach (var model in models)
                {
                    results.Add(await StudentIncidentSummaryModel.GetSummary(unitOfWork, model));
                }

                return results;
            }
        }

        public async Task<StudentIncidentModel> GetIncidentById(Guid incidentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var incident = await unitOfWork.StudentIncidents.GetById(incidentId);

                return new StudentIncidentModel(incident);
            }
        }

        public async Task<int> GetBehaviourPointsByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var points = await unitOfWork.StudentIncidents.GetPointsByStudent(studentId, academicYearId);

                return points;
            }
        }

        public async Task<int> GetBehaviourCountByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var count = await unitOfWork.StudentIncidents.GetCountByStudent(studentId, academicYearId);

                return count;
            }
        }

        public async Task CreateIncident(IncidentRequestModel incident)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var studentIncident = new StudentIncident
                {
                    Points = incident.Points,
                    OutcomeId = incident.OutcomeId,
                    StatusId = incident.StatusId,
                    StudentId = incident.StudentId,
                    Incident = new Incident
                    {
                        CreatedDate = DateTime.Now,
                        BehaviourTypeId = incident.BehaviourTypeId,
                        LocationId = incident.LocationId,
                        CreatedById = incident.CreatedById,
                        Comments = incident.Comments,
                        AcademicYearId = incident.AcademicYearId,
                    }
                };

                unitOfWork.StudentIncidents.Create(studentIncident);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateIncident(Guid incidentId, IncidentRequestModel incident)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var studentIncidentInDb = await unitOfWork.StudentIncidents.GetById(incidentId);

                studentIncidentInDb.Points = incident.Points;
                studentIncidentInDb.Incident.BehaviourTypeId = incident.BehaviourTypeId;
                studentIncidentInDb.Incident.LocationId = incident.LocationId;
                studentIncidentInDb.OutcomeId = incident.OutcomeId;
                studentIncidentInDb.StatusId = incident.StatusId;
                studentIncidentInDb.Incident.Comments = incident.Comments;

                await unitOfWork.StudentIncidents.Update(studentIncidentInDb);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteIncident(Guid incidentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                await unitOfWork.Incidents.Delete(incidentId);

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

        public async Task<IEnumerable<BehaviourRoleTypeModel>> GetRoleTypes()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var roleTypes = await unitOfWork.BehaviourRoleTypes.GetAll();

                return roleTypes.Select(r => new BehaviourRoleTypeModel(r)).ToList();
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

        public async Task CreateDetention(DetentionRequestModel model)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
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

                DateTime? nextOccurrence = model.StartTime.GetNextOccurrence(model.Frequency);
                TimeSpan duration = model.EndTime - model.StartTime;

                while (nextOccurrence != null && nextOccurrence.Value < model.LastOccurrence)
                {
                    var nextDetention = new Detention
                    {
                        DetentionTypeId = model.DetentionTypeId,
                        SupervisorId = model.SameSupervisor ? model.SupervisorId : null,
                        Event = new DiaryEvent
                        {
                            StartTime = nextOccurrence.Value,
                            EndTime = nextOccurrence.Value.Add(duration),
                            RoomId = model.RoomId,
                            EventTypeId = EventTypes.Detention,
                            Subject = "Detention"
                        }
                    };

                    unitOfWork.Detentions.Create(nextDetention);
                    await unitOfWork.BatchSaveChangesAsync();

                    nextOccurrence = nextOccurrence.Value.GetNextOccurrence(model.Frequency);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetention(Guid detentionId, DetentionRequestModel detention)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var detentionInDb = await unitOfWork.Detentions.GetById(detentionId);

                detentionInDb.DetentionTypeId = detention.DetentionTypeId;
                detentionInDb.Event.StartTime = detention.StartTime;
                detentionInDb.Event.EndTime = detention.EndTime;
                detentionInDb.Event.RoomId = detention.RoomId;

                await unitOfWork.Detentions.Update(detentionInDb);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteDetention(Guid detentionId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                await unitOfWork.Detentions.Delete(detentionId);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<StudentIncidentSummaryModel>> GetInvolvedStudentsByIncident(Guid incidentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var involvedStudents =
                    (await unitOfWork.StudentIncidents.GetByIncident(incidentId)).Select(s =>
                        new StudentIncidentModel(s)).ToList();

                var results = new List<StudentIncidentSummaryModel>();

                foreach (var involvedStudent in involvedStudents)
                {
                    results.Add(await StudentIncidentSummaryModel.GetSummary(unitOfWork, involvedStudent));
                }

                return results;
            }
        }

        public async Task AddStudentToIncident(StudentIncidentRequestModel model)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var studentIncident = new StudentIncident
                {
                    IncidentId = model.IncidentId,
                    OutcomeId = model.OutcomeId,
                    StatusId = model.StatusId,
                    RoleTypeId = model.RoleTypeId,
                    StudentId = model.StudentId,
                    Points = model.Points
                };

                unitOfWork.StudentIncidents.Create(studentIncident);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task RemoveStudentFromIncident(Guid studentIncidentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var studentIncident = await unitOfWork.StudentIncidents.GetById(studentIncidentId);

                if (studentIncident == null)
                {
                    throw new NotFoundException("Student incident not found.");
                }

                var studentCount = await unitOfWork.StudentIncidents.GetCountByIncident(studentIncident.IncidentId);

                if (studentCount < 2)
                {
                    throw new LogicException("Cannot remove the only student from this incident.");
                }

                await unitOfWork.StudentIncidents.Delete(studentIncidentId);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task AddToDetention(Guid detentionId, Guid studentIncidentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var incidentDetention = new StudentIncidentDetention
                {
                    DetentionId = detentionId,
                    StudentIncidentId = studentIncidentId
                };

                unitOfWork.IncidentDetentions.Create(incidentDetention);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task RemoveFromDetention(Guid detentionId, Guid studentIncidentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var relatedIncident =
                    await unitOfWork.IncidentDetentions.GetByStudentIncident(detentionId, studentIncidentId);

                if (relatedIncident == null)
                {
                    throw new NotFoundException("Detention not found.");
                }

                await unitOfWork.IncidentDetentions.Delete(relatedIncident.Id);

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
