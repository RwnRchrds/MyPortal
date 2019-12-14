using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateAchievement(Achievement achievement)
        {
            achievement.Date = DateTime.Today;

            ValidationService.ValidateModel(achievement);

            if (!await UnitOfWork.AcademicYears.Any(x => x.Id == achievement.AcademicYearId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Academic year not found");
            }

            UnitOfWork.Achievements.Add(achievement);
            await UnitOfWork.Complete();
        }

        public async Task CreateBehaviourIncident(Incident incident)
        {
            incident.Date = DateTime.Today;

            ValidationService.ValidateModel(incident);

            if (!await UnitOfWork.AcademicYears.Any(x => x.Id == incident.AcademicYearId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Academic year not found");
            }

            UnitOfWork.Incidents.Add(incident);
            await UnitOfWork.Complete();
        }

        public async Task DeleteAchievement(int achievementId)
        {
            var achievement = await GetAchievementById(achievementId);

            UnitOfWork.Achievements.Remove(achievement);

            await UnitOfWork.Complete();
        }

        public async Task DeleteBehaviourIncident(int incidentId)
        {
            var incident = await GetBehaviourIncidentById(incidentId);

            UnitOfWork.Incidents.Remove(incident);

            await UnitOfWork.Complete();
        }

        public async Task<Achievement> GetAchievementById(int achievementId)
        {
            var achievement = await UnitOfWork.Achievements.GetById(achievementId);

            if (achievement == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Achievement not found");
            }

            return achievement;
        }

        public async Task<int> GetAchievementCountByStudent(int studentId, int academicYearId)
        {
            if (!await UnitOfWork.Students.Any(x => x.Id == studentId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Student not found");
            }

            var achievementCount =
                await UnitOfWork.Achievements.GetCountByStudent(studentId, academicYearId);

            if (achievementCount < 0)
            {
                throw new ServiceException(ExceptionType.BadRequest,"Cannot have negative achievement count");
            }

            return achievementCount;
        }

        public async Task<int> GetAchievementPointsCountByStudent(int studentId, int academicYearId)
        {
            if (!await UnitOfWork.Students.Any(x => x.Id == studentId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Student not found");
            }

            var points =
                await UnitOfWork.Achievements.GetPointsByStudent(studentId, academicYearId);

            if (points < 0)
            {
                throw new ServiceException(ExceptionType.BadRequest,"Cannot have negative points count");
            }

            return points;
        }

        public async Task<Incident> GetBehaviourIncidentById(int incidentId)
        {
            var incident = await UnitOfWork.Incidents.GetById(incidentId);

            if (incident == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Incident not found");
            }

            return incident;
        }

        public async Task<int> GetBehaviourIncidentCountByStudent(int studentId, int academicYearId)
        {
            if (! await UnitOfWork.Students.Any(x => x.Id == studentId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Student not found");
            }

            var negPoints =
                await UnitOfWork.Incidents.GetCountByStudent(studentId, academicYearId);

            if (negPoints < 0)
            {
                throw new ServiceException(ExceptionType.BadRequest,"Cannot have negative incident count");
            }

            return negPoints;
        }

        public async Task<int> GetBehaviourPointsCountByStudent(int studentId, int academicYearId)
        {
            if (!await UnitOfWork.Students.Any(x => x.Id == studentId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Student not found");
            }

            var points =
                await UnitOfWork.Incidents.GetPointsByStudent(studentId,
                    academicYearId);

            if (points < 0)
            {
                throw new ServiceException(ExceptionType.BadRequest,"Cannot have negative points count");
            }

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
            if (!await UnitOfWork.Students.Any(x => x.Id == studentId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Student not found");
            }

            var achievementPoints =
                await GetAchievementPointsCountByStudent(studentId, academicYearId);

            var behaviourPoints =
                await GetBehaviourPointsCountByStudent(studentId,
                    academicYearId);

            return achievementPoints - behaviourPoints;
        }

        public async Task<IEnumerable<Achievement>> GetAchievementsByStudent(int studentId, int academicYearId)
        {
            return await UnitOfWork.Achievements.GetByStudent(studentId, academicYearId);
        }

        public async Task<IEnumerable<Incident>> GetBehaviourIncidentsByStudent(int studentId,
            int academicYearId)
        {
            return await UnitOfWork.Incidents.GetByStudent(studentId, academicYearId);
        }
        
        public async Task UpdateAchievement(Achievement achievement)
        {
            var achievementInDb = await GetAchievementById(achievement.Id);

            achievementInDb.LocationId = achievement.LocationId;
            achievementInDb.Comments = achievement.Comments;
            achievementInDb.Points = achievement.Points;
            achievementInDb.Resolved = achievement.Resolved;
            achievementInDb.AchievementTypeId = achievement.AchievementTypeId;

            await UnitOfWork.Complete();
        }

        public async Task UpdateBehaviourIncident(Incident incident)
        {
            var incidentInDb = await GetBehaviourIncidentById(incident.Id);

            incidentInDb.LocationId = incident.LocationId;
            incidentInDb.Comments = incident.Comments;
            incidentInDb.Points = incident.Points;
            incidentInDb.Resolved = incident.Resolved;
            incidentInDb.BehaviourTypeId = incident.BehaviourTypeId;

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<AchievementType>> GetAchievementTypes()
        {
            var achievementTypes = await UnitOfWork.AchievementTypes.GetAll(x => x.Description);

            return achievementTypes;
        }

        public async Task<IEnumerable<IncidentType>> GetBehaviourIncidentTypes()
        {
            var incidentTypes = await UnitOfWork.IncidentTypes.GetAll(x => x.Description);

            return incidentTypes;
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