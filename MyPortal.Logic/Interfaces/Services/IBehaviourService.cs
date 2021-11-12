using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;
using MyPortal.Logic.Models.Requests.Behaviour.Detentions;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IBehaviourService
    {
        #region Achievements
        Task<IEnumerable<AchievementModel>> GetAchievementsByStudent(Guid studentId, Guid academicYearId);
        Task<AchievementModel> GetAchievementById(Guid achievementId);
        Task<int> GetAchievementPointsByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetAchievementCountByStudent(Guid studentId, Guid academicYearId);
        Task CreateAchievement(params CreateAchievementModel[] achievements);
        Task UpdateAchievement(params UpdateAchievementModel[] achievements);
        Task DeleteAchievement(params Guid[] achievementIds);
        Task<IEnumerable<AchievementTypeModel>> GetAchievementTypes();
        Task<IEnumerable<AchievementOutcomeModel>> GetAchievementOutcomes();    
        #endregion
        
        #region Incidents
        Task<IEnumerable<IncidentModel>> GetIncidentsByStudent(Guid studentId, Guid academicYearId);
        Task<IncidentModel> GetIncidentById(Guid incidentId);
        Task<int> GetBehaviourPointsByStudent(Guid studentId, Guid academicYearId);
        Task<int> GetBehaviourCountByStudent(Guid studentId, Guid academicYearId);
        Task CreateIncident(params CreateIncidentModel[] incidents);
        Task UpdateIncident(params UpdateIncidentModel[] incidents);
        Task DeleteIncident(params Guid[] incidentIds);
        Task<IEnumerable<IncidentTypeModel>> GetIncidentTypes();
        Task<IEnumerable<BehaviourOutcomeModel>> GetIncidentOutcomes();
        Task<IEnumerable<BehaviourStatusModel>> GetBehaviourStatus();
        #endregion
        
        #region Detentions
        Task<IEnumerable<DetentionModel>> Get(DetentionSearchOptions searchOptions);
        Task<DetentionModel> GetById(Guid detentionId);
        Task<DetentionModel> GetByIncident(Guid incidentId);
        Task Create(params CreateDetentionRequest[] detentionModels);
        Task Update(params UpdateDetentionRequest[] detentionModels);
        Task Delete(params Guid[] detentionIds);
        Task AddStudent(Guid detentionId, Guid incidentId);
        Task RemoveStudent(Guid incidentDetentionId);
        #endregion
    }
}
