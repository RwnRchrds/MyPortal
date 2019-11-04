using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Services
{
    public class BehaviourService : MyPortalService
    {
        public BehaviourService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task CreateAchievement(BehaviourAchievement achievement)
        {
            achievement.Date = DateTime.Today;

            if (!ValidationService.ModelIsValid(achievement))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            if (!await UnitOfWork.CurriculumAcademicYears.AnyAsync(x => x.Id == achievement.AcademicYearId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            UnitOfWork.BehaviourAchievements.Add(achievement);
            await UnitOfWork.Complete();
        }

        public async Task CreateBehaviourIncident(BehaviourIncident incident)
        {
            incident.Date = DateTime.Today;

            if (!ValidationService.ModelIsValid(incident))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            if (!await UnitOfWork.CurriculumAcademicYears.AnyAsync(x => x.Id == incident.AcademicYearId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            UnitOfWork.BehaviourIncidents.Add(incident);
            await UnitOfWork.Complete();
        }

        public async Task DeleteAchievement(int achievementId)
        {
            var achievement = await GetAchievementById(achievementId);

            achievement.Deleted = true; //Flag as deleted

            //_unitOfWork.BehaviourAchievements.Remove(achievement); //Enable this to delete from the database
            await UnitOfWork.Complete();
        }

        public async Task DeleteBehaviourIncident(int incidentId)
        {
            var incident = await GetBehaviourIncidentById(incidentId);

            incident.Deleted = true;

            //__unitOfWork.BehaviourIncidents.Remove(incident);
            await UnitOfWork.Complete();
        }

        public async Task<BehaviourAchievement> GetAchievementById(int achievementId)
        {
            var achievement = await UnitOfWork.BehaviourAchievements.GetByIdAsync(achievementId);

            if (achievement == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Achievement not found");
            }

            return achievement;
        }

        public async Task<int> GetAchievementCountByStudent(int studentId, int academicYearId)
        {
            if (!await UnitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            var achievementCount =
                await UnitOfWork.BehaviourAchievements.GetAchievementCountByStudent(studentId, academicYearId);

            if (achievementCount < 0)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot have negative achievement count");
            }

            return achievementCount;
        }

        public async Task<int> GetAchievementPointsCountByStudent(int studentId, int academicYearId)
        {
            if (!await UnitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            var points =
                await UnitOfWork.BehaviourAchievements.GetAchievementPointsCountByStudent(studentId, academicYearId);

            if (points < 0)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot have negative points count");
            }

            return points;
        }

        public async Task<BehaviourIncident> GetBehaviourIncidentById(int incidentId)
        {
            var incident = await UnitOfWork.BehaviourIncidents.GetByIdAsync(incidentId);

            if (incident == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Incident not found");
            }

            return incident;
        }

        public async Task<int> GetBehaviourIncidentCountByStudent(int studentId, int academicYearId)
        {
            if (! await UnitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            var negPoints =
                await UnitOfWork.BehaviourIncidents.GetBehaviourIncidentCountByStudent(studentId, academicYearId);

            if (negPoints < 0)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot have negative incident count");
            }

            return negPoints;
        }

        public async Task<int> GetBehaviourPointsCountByStudent(int studentId, int academicYearId)
        {
            if (!await UnitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            var points =
                await UnitOfWork.BehaviourIncidents.GetBehaviourIncidentPointsCountByStudent(studentId,
                    academicYearId);

            if (points < 0)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot have negative points count");
            }

            return points;
        }

        public async Task<IEnumerable<ChartDataCategoric>> GetChartDataAchievementsByType(int academicYearId)
        {
            var recordedAchievementTypes =
                await UnitOfWork.BehaviourAchievementTypes.GetAllRecordedAchievementTypes(academicYearId);

            return recordedAchievementTypes.Select(achievementType => new ChartDataCategoric(achievementType.Description, achievementType.Achievements.Count)).ToList();
        }

        public async Task<IEnumerable<ChartDataCategoric>> GetChartDataBehaviourIncidentsByType(int academicYearId)
        {
            var recordedBehaviourTypes =
                await UnitOfWork.BehaviourIncidentTypes.GetAllRecordedIncidentTypes(academicYearId);

            return recordedBehaviourTypes.Select(behaviourType => new ChartDataCategoric(behaviourType.Description, behaviourType.Incidents.Count)).ToList();
        }

        public async Task<int> GetTotalConductPointsByStudent(int studentId, int academicYearId)
        {
            if (!await UnitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            var achievementPoints =
                await GetAchievementPointsCountByStudent(studentId, academicYearId);

            var behaviourPoints =
                await GetBehaviourPointsCountByStudent(studentId,
                    academicYearId);

            return achievementPoints - behaviourPoints;
        }

        public async Task<IEnumerable<BehaviourAchievement>> GetAchievementsByStudent(int studentId, int academicYearId)
        {
            return await UnitOfWork.BehaviourAchievements.GetAchievementsByStudent(studentId, academicYearId);
        }

        public async Task<IEnumerable<BehaviourIncident>> GetBehaviourIncidentsByStudent(int studentId,
            int academicYearId)
        {
            return await UnitOfWork.BehaviourIncidents.GetBehaviourIncidentsByStudent(studentId, academicYearId);
        }
        
        public async Task UpdateAchievement(BehaviourAchievement achievement)
        {
            var achievementInDb = await GetAchievementById(achievement.Id);

            achievementInDb.LocationId = achievement.LocationId;
            achievementInDb.Comments = achievement.Comments;
            achievementInDb.Points = achievement.Points;
            achievementInDb.Resolved = achievement.Resolved;
            achievementInDb.AchievementTypeId = achievement.AchievementTypeId;

            await UnitOfWork.Complete();
        }

        public async Task UpdateBehaviourIncident(BehaviourIncident incident)
        {
            var incidentInDb = await GetBehaviourIncidentById(incident.Id);

            incidentInDb.LocationId = incident.LocationId;
            incidentInDb.Comments = incident.Comments;
            incidentInDb.Points = incident.Points;
            incidentInDb.Resolved = incident.Resolved;
            incidentInDb.BehaviourTypeId = incident.BehaviourTypeId;

            await UnitOfWork.Complete();
        }

        public async Task<int> GetAchievementPointsToday()
        {
            return await UnitOfWork.BehaviourAchievements.GetBehaviourAchievementPointsToday();
        }

        public async Task<int> GetBehaviourPointsToday()
        {
            return await UnitOfWork.BehaviourIncidents.GetBehaviourIncidentPointsToday();
        }
    }
}