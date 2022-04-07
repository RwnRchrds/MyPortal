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

        public async Task CreateAchievement(Guid userId, params CreateAchievementModel[] requests)
        {
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
                        CreatedById = userId,
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
        
        public async Task<IEnumerable<StudentIncidentSummaryModel>> GetIncidentsByStudent(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var incidents = await unitOfWork.StudentIncidents.GetByStudent(studentId, academicYearId);

                var models = incidents.Select(i => new StudentIncidentModel(i));

                var results = new List<StudentIncidentSummaryModel>();

                foreach (var model in models)
                {
                    results.Add(await model.ToListModel(unitOfWork));
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

        public async Task CreateIncident(Guid userId, params CreateIncidentModel[] incidents)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var incidentModel in incidents)
                {
                    var studentIncident = new StudentIncident
                    {
                        Points = incidentModel.Points,
                        OutcomeId = incidentModel.OutcomeId,
                        StatusId = incidentModel.StatusId,
                        StudentId = incidentModel.StudentId,
                        Incident = new Incident
                        {
                            CreatedDate = DateTime.Now,
                            BehaviourTypeId = incidentModel.BehaviourTypeId,
                            LocationId = incidentModel.LocationId,
                            CreatedById = userId,
                            Comments = incidentModel.Comments,
                            AcademicYearId = incidentModel.AcademicYearId,
                        }
                    };

                    unitOfWork.StudentIncidents.Create(studentIncident);
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
                    var studentIncidentInDb = await unitOfWork.StudentIncidents.GetById(incidentModel.Id);

                    studentIncidentInDb.Points = incidentModel.Points;
                    studentIncidentInDb.Incident.BehaviourTypeId = incidentModel.BehaviourTypeId;
                    studentIncidentInDb.Incident.LocationId = incidentModel.LocationId;
                    studentIncidentInDb.OutcomeId = incidentModel.OutcomeId;
                    studentIncidentInDb.StatusId = incidentModel.StatusId;
                    studentIncidentInDb.Incident.Comments = incidentModel.Comments;

                    await unitOfWork.StudentIncidents.Update(studentIncidentInDb);
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

        public async Task CreateDetention(params CreateDetentionRequest[] detentionModels)
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
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDetention(params UpdateDetentionRequest[] detentionModels)
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

        public async Task DeleteDetention(params Guid[] detentionIds)
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
                    await involvedStudent.Student.Load(unitOfWork);
                    await involvedStudent.Incident.Load(unitOfWork);
                    
                    results.Add(new StudentIncidentSummaryModel(involvedStudent));
                }

                return results;
            }
        }

        public async Task AddStudentToIncident(params AddToIncidentModel[] models)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var model in models)
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
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task RemoveStudentFromIncident(params Guid[] studentIncidentIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var studentIncidentId in studentIncidentIds)
                {
                    var studentIncident = await unitOfWork.StudentIncidents.GetById(studentIncidentId);

                    if (studentIncident == null)
                    {
                        throw new NotFoundException("Student incident not found.");
                    }

                    var studentCount = await unitOfWork.StudentIncidents.GetCountByIncident(studentIncident.IncidentId);

                    if (studentCount < 2)
                    {
                        throw new LogicException("Cannot remove the only student from incident.");
                    }

                    await unitOfWork.StudentIncidents.Delete(studentIncidentId);
                }

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

        public async Task RemoveFromDetention(Guid incidentDetentionId)
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
    }
}
