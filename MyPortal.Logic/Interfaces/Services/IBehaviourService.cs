using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Data.Behaviour.Achievements;
using MyPortal.Logic.Models.Data.Behaviour.Detentions;
using MyPortal.Logic.Models.Data.Behaviour.Incidents;
using MyPortal.Logic.Models.Data.Students;
using MyPortal.Logic.Models.Requests.Behaviour.Achievements;
using MyPortal.Logic.Models.Requests.Behaviour.Detentions;
using MyPortal.Logic.Models.Requests.Behaviour.Incidents;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IBehaviourService : IService
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

        Task<IEnumerable<DetentionModel>> GetDetentions(DetentionSearchOptions searchOptions);
        Task<DetentionModel> GetDetentionById(Guid detentionId);
        Task<DetentionModel> GetDetentionByIncident(Guid incidentId);
        Task<DetentionModel> CreateDetention(DetentionRequestModel detentionModel);
        Task UpdateDetention(Guid detentionId, DetentionRequestModel detentionModel);
        Task DeleteDetention(Guid detentionId);
        Task AddDetentions(Guid studentIncidentId, Guid[] detentionIds);
        Task RemoveDetentions(Guid studentIncidentId, Guid[] detentionIds);

        #endregion
    }
}