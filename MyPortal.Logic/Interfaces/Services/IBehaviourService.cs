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
        Task<StudentAchievementModel> GetStudentAchievementById(Guid achievementId);
        Task<int> GetAchievementPointsByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetAchievementCountByStudent(Guid studentId, Guid academicYearId);
        Task<StudentAchievementModel> CreateAchievement(AchievementRequestModel achievement);
        Task UpdateAchievement(Guid achievementId, AchievementRequestModel achievement);
        Task DeleteAchievement(Guid achievementId);
        Task<IEnumerable<AchievementTypeModel>> GetAchievementTypes();
        Task<IEnumerable<AchievementOutcomeModel>> GetAchievementOutcomes();    
        #endregion
        
        #region Incidents
        Task<IEnumerable<StudentIncidentSummaryModel>> GetIncidentsByStudent(Guid studentId, Guid academicYearId);
        Task<StudentIncidentModel> GetIncidentById(Guid incidentId);
        Task<int> GetBehaviourPointsByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetBehaviourCountByStudent(Guid studentId, Guid academicYearId);
        Task<StudentIncidentModel> CreateIncident(IncidentRequestModel incident);
        Task UpdateIncident(Guid incidentId, IncidentRequestModel incident);
        Task DeleteIncident(Guid incidentId);
        Task AddStudentToIncident(StudentIncidentRequestModel model);
        Task<IEnumerable<IncidentTypeModel>> GetIncidentTypes();
        Task<IEnumerable<BehaviourRoleTypeModel>> GetRoleTypes();
        Task<IEnumerable<BehaviourOutcomeModel>> GetIncidentOutcomes();
        Task<IEnumerable<BehaviourStatusModel>> GetBehaviourStatus();
        Task RemoveStudentFromIncident(Guid studentIncidentId);
        Task<IEnumerable<StudentIncidentSummaryModel>> GetInvolvedStudentsByIncident(Guid incidentId);
        #endregion
        
        #region Detentions
        Task<IEnumerable<DetentionModel>> Get(DetentionSearchOptions searchOptions);
        Task<DetentionModel> GetById(Guid detentionId);
        Task<DetentionModel> GetByIncident(Guid incidentId);
        Task<DetentionModel> CreateDetention(DetentionRequestModel detentionModel);
        Task UpdateDetention(Guid detentionId, DetentionRequestModel detentionModel);
        Task DeleteDetention(Guid detentionId);
        Task AddToDetention(Guid detentionId, Guid studentIncidentId);
        Task RemoveFromDetention(Guid detentionId, Guid studentIncidentId);
        #endregion
    }
}
