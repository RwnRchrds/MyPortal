using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.BusinessLogic.Models;
using MyPortal.BusinessLogic.Models.Data;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class BehaviourService : MyPortalService
    {
        public BehaviourService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public BehaviourService() : base()
        {

        }

        public async Task CreateAchievement(AchievementDto achievement)
        {
            achievement.Date = DateTime.Today;

            ValidationService.ValidateModel(achievement);

            UnitOfWork.Achievements.Add(Mapping.Map<Achievement>(achievement));
            await UnitOfWork.Complete();
        }

        public async Task CreateBehaviourIncident(IncidentDto incident)
        {
            incident.Date = DateTime.Today;

            ValidationService.ValidateModel(incident);

            UnitOfWork.Incidents.Add(Mapping.Map<Incident>(incident));
            await UnitOfWork.Complete();
        }

        public async Task DeleteAchievement(int achievementId)
        {
            var achievement = await UnitOfWork.Achievements.GetById(achievementId);

            if (achievement == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Achievement not found.");
            }

            UnitOfWork.Achievements.Remove(achievement);

            await UnitOfWork.Complete();
        }

        public async Task DeleteBehaviourIncident(int incidentId)
        {
            var incident = await UnitOfWork.Incidents.GetById(incidentId);

            if (incident == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Incident not found.");
            }

            UnitOfWork.Incidents.Remove(incident);

            await UnitOfWork.Complete();
        }

        public async Task<AchievementDto> GetAchievementById(int achievementId)
        {
            var achievement = await UnitOfWork.Achievements.GetById(achievementId);

            if (achievement == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Achievement not found.");
            }

            return Mapping.Map<AchievementDto>(achievement);
        }

        public async Task<int> GetAchievementCountByStudent(int studentId, int academicYearId)
        {
            var achievementCount =
                await UnitOfWork.Achievements.GetCountByStudent(studentId, academicYearId);

            return achievementCount;
        }

        public async Task<int> GetAchievementPointsCountByStudent(int studentId, int academicYearId)
        {
            var points =
                await UnitOfWork.Achievements.GetPointsByStudent(studentId, academicYearId);

            return points;
        }

        public async Task<IncidentDto> GetBehaviourIncidentById(int incidentId)
        {
            var incident = await UnitOfWork.Incidents.GetById(incidentId);

            if (incident == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Incident not found.");
            }

            return Mapping.Map<IncidentDto>(incident);
        }

        public async Task<int> GetBehaviourIncidentCountByStudent(int studentId, int academicYearId)
        {
            var negPoints =
                await UnitOfWork.Incidents.GetCountByStudent(studentId, academicYearId);

            return negPoints;
        }

        public async Task<int> GetBehaviourPointsCountByStudent(int studentId, int academicYearId)
        {
            var points =
                await UnitOfWork.Incidents.GetPointsByStudent(studentId,
                    academicYearId);

            return points;
        }

        public async Task<IEnumerable<ChartDataCategoric>> GetChartDataAchievementsByType(int academicYearId)
        {
            var recordedAchievementTypes =
                await UnitOfWork.AchievementTypes.GetRecorded(academicYearId);

            return recordedAchievementTypes.Select(achievementType => new ChartDataCategoric(achievementType.Description, achievementType.Achievements.Count)).ToList();
        }

        public async Task<IEnumerable<ChartDataCategoric>> GetChartDataBehaviourIncidentsByType(int academicYearId)
        {
            var recordedBehaviourTypes =
                await UnitOfWork.IncidentTypes.GetRecorded(academicYearId);

            return recordedBehaviourTypes.Select(behaviourType => new ChartDataCategoric(behaviourType.Description, behaviourType.Incidents.Count)).ToList();
        }

        public async Task<int> GetTotalConductPointsByStudent(int studentId, int academicYearId)
        {
            var achievementPoints =
                await GetAchievementPointsCountByStudent(studentId, academicYearId);

            var behaviourPoints =
                await GetBehaviourPointsCountByStudent(studentId,
                    academicYearId);

            return achievementPoints - behaviourPoints;
        }

        public async Task<IEnumerable<AchievementDto>> GetAchievementsByStudent(int studentId, int academicYearId)
        {
            return (await UnitOfWork.Achievements.GetByStudent(studentId, academicYearId)).Select(Mapping.Map<AchievementDto>);
        }

        public async Task<IEnumerable<IncidentDto>> GetBehaviourIncidentsByStudent(int studentId,
            int academicYearId)
        {
            return (await UnitOfWork.Incidents.GetByStudent(studentId, academicYearId)).Select(Mapping.Map<IncidentDto>);
        }
        
        public async Task UpdateAchievement(AchievementDto achievement)
        {
            var achievementInDb = await UnitOfWork.Achievements.GetById(achievement.Id);

            achievementInDb.LocationId = achievement.LocationId;
            achievementInDb.Comments = achievement.Comments;
            achievementInDb.Points = achievement.Points;
            achievementInDb.Resolved = achievement.Resolved;
            achievementInDb.AchievementTypeId = achievement.AchievementTypeId;

            await UnitOfWork.Complete();
        }

        public async Task UpdateBehaviourIncident(IncidentDto incident)
        {
            var incidentInDb = await UnitOfWork.Incidents.GetById(incident.Id);

            incidentInDb.LocationId = incident.LocationId;
            incidentInDb.Comments = incident.Comments;
            incidentInDb.Points = incident.Points;
            incidentInDb.Resolved = incident.Resolved;
            incidentInDb.BehaviourTypeId = incident.BehaviourTypeId;

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<AchievementTypeDto>> GetAchievementTypes()
        {
            return (await UnitOfWork.AchievementTypes.GetAll(x => x.Description)).Select(
                Mapping.Map<AchievementTypeDto>);
        }

        public async Task<IEnumerable<IncidentTypeDto>> GetBehaviourIncidentTypes()
        {
            return (await UnitOfWork.IncidentTypes.GetAll(x => x.Description)).Select(Mapping.Map<IncidentTypeDto>);
        }

        public async Task<int> GetAchievementPointsToday()
        {
            return await UnitOfWork.Achievements.GetPointsToday();
        }

        public async Task<int> GetBehaviourPointsToday()
        {
            return await UnitOfWork.Incidents.GetPointsToday();
        }
    }
}