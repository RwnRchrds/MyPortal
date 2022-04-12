using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;
using MyPortal.Logic.Models.Requests.Behaviour.Detentions;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IBehaviourService
    {
        #region Achievements
        Task<IEnumerable<StudentAchievementSummaryModel>> GetAchievementsByStudent(Guid studentId, Guid academicYearId);
        Task<StudentAchievementModel> GetAchievementById(Guid achievementId);
        Task<int> GetAchievementPointsByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetAchievementCountByStudent(Guid studentId, Guid academicYearId);
        Task CreateAchievement(Guid userId, params CreateAchievementModel[] achievements);
        Task UpdateAchievement(params UpdateAchievementModel[] achievements);
        Task DeleteAchievement(params Guid[] achievementIds);
        Task<IEnumerable<AchievementTypeModel>> GetAchievementTypes();
        Task<IEnumerable<AchievementOutcomeModel>> GetAchievementOutcomes();    
        #endregion
        
        #region Incidents
        Task<IEnumerable<StudentIncidentSummaryModel>> GetIncidentsByStudent(Guid studentId, Guid academicYearId);
        Task<StudentIncidentModel> GetIncidentById(Guid incidentId);
        Task<int> GetBehaviourPointsByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetBehaviourCountByStudent(Guid studentId, Guid academicYearId);
        Task CreateIncident(Guid userId, params CreateIncidentModel[] incidents);
        Task UpdateIncident(params UpdateIncidentModel[] incidents);
        Task DeleteIncident(params Guid[] incidentIds);
        Task AddStudentToIncident(params AddToIncidentModel[] models);
        Task<IEnumerable<IncidentTypeModel>> GetIncidentTypes();
        Task<IEnumerable<BehaviourRoleTypeModel>> GetRoleTypes();
        Task<IEnumerable<BehaviourOutcomeModel>> GetIncidentOutcomes();
        Task<IEnumerable<BehaviourStatusModel>> GetBehaviourStatus();
        Task RemoveStudentFromIncident(params Guid[] studentIncidentIds);
        Task<IEnumerable<StudentIncidentSummaryModel>> GetInvolvedStudentsByIncident(Guid incidentId);
        #endregion
        
        #region Detentions
        Task<IEnumerable<DetentionModel>> Get(DetentionSearchOptions searchOptions);
        Task<DetentionModel> GetById(Guid detentionId);
        Task<DetentionModel> GetByIncident(Guid incidentId);
        Task CreateDetention(params CreateDetentionRequest[] detentionModels);
        Task UpdateDetention(params UpdateDetentionRequest[] detentionModels);
        Task DeleteDetention(params Guid[] detentionIds);
        Task AddToDetention(Guid detentionId, Guid incidentId);
        Task RemoveFromDetention(Guid incidentDetentionId);
        #endregion
    }
}
