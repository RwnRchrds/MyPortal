using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
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

            if (!await _unitOfWork.CurriculumAcademicYears.AnyAsync(x => x.Id == achievement.AcademicYearId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            if (!await achievement.Date.IsInAcademicYear(achievement.AcademicYearId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Date is not in academic year");
            }

            _unitOfWork.BehaviourAchievements.Add(achievement);
            await _unitOfWork.Complete();
        }

        public async Task CreateBehaviourIncident(BehaviourIncident incident)
        {
            incident.Date = DateTime.Today;

            if (!ValidationService.ModelIsValid(incident))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            if (!await _unitOfWork.CurriculumAcademicYears.AnyAsync(x => x.Id == incident.AcademicYearId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            if (!await incident.Date.IsInAcademicYear(_unitOfWork, incident.AcademicYearId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Date is not in academic year");
            }

            _unitOfWork.BehaviourIncidents.Add(incident);
            await _unitOfWork.Complete();
        }

        public async Task DeleteAchievement(int achievementId)
        {
            var achievement = await _unitOfWork.BehaviourAchievements.GetByIdAsync(achievementId);

            if (achievement == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Achievement not found");
            }

            achievement.Deleted = true; //Flag as deleted

            //_unitOfWork.BehaviourAchievements.Remove(achievement); //Enable this to delete from the database
            await _unitOfWork.Complete();
        }

        public async Task DeleteBehaviourIncident(int incidentId)
        {
            var incident = await _unitOfWork.BehaviourIncidents.GetByIdAsync(incidentId);

            if (incident == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Incident not found");
            }

            incident.Deleted = true;

            //__unitOfWork.BehaviourIncidents.Remove(incident);
            await _unitOfWork.Complete();
        }

        public async Task<BehaviourAchievementDto> GetAchievementById(int achievementId)
        {
            var achievement = await _unitOfWork.BehaviourAchievements.GetByIdAsync(achievementId);

            if (achievement == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Achievement not found");
            }

            return Mapper.Map<BehaviourAchievement, BehaviourAchievementDto>(achievement);
        }

        public async Task<int> GetAchievementCountByStudent(int studentId, int academicYearId)
        {
            if (!await _unitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            var achievementCount =
                await _unitOfWork.BehaviourAchievements.GetAchievementCountByStudent(studentId, academicYearId);

            if (achievementCount < 0)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot have negative achievement count");
            }

            return achievementCount;
        }

        public async Task<int> GetAchievementPointsCountByStudent(int studentId, int academicYearId)
        {
            if (!await _unitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            var points =
                await _unitOfWork.BehaviourAchievements.GetAchievementPointsCountByStudent(studentId, academicYearId);

            if (points < 0)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot have negative points count");
            }

            return points;
        }

        public async Task<IEnumerable<GridBehaviourAchievementDto>> GetAchievementsByStudentDataGrid(int studentId,
            int academicYearId)
        {
            var achievements =
                await _unitOfWork.BehaviourAchievements.GetAchievementsByStudent(studentId, academicYearId);

            return achievements.Select(Mapper.Map<BehaviourAchievement, GridBehaviourAchievementDto>);
        }

        public async Task<BehaviourIncidentDto> GetBehaviourIncidentById(int incidentId)
        {
            var incident = await _unitOfWork.BehaviourIncidents.GetByIdAsync(incidentId);

            if (incident == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Incident not found");
            }

            return Mapper.Map<BehaviourIncident, BehaviourIncidentDto>(incident);
        }

        public async Task<int> GetBehaviourIncidentCountByStudent(int studentId, int academicYearId)
        {
            if (! await _unitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            var negPoints =
                await _unitOfWork.BehaviourIncidents.GetBehaviourIncidentCountByStudent(studentId, academicYearId);

            if (negPoints < 0)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot have negative incident count");
            }

            return negPoints;
        }

        public async Task<IEnumerable<GridBehaviourIncidentDto>> GetBehaviourIncidentsForGrid(int studentId,
            int academicYearId)
        {
            var incidents = await _unitOfWork.BehaviourIncidents.GetBehaviourIncidentsByStudent(studentId, academicYearId);

            return incidents.Select(Mapper.Map<BehaviourIncident, GridBehaviourIncidentDto>).ToList();
        }

        public async Task<int> GetBehaviourPointsCountByStudent(int studentId, int academicYearId)
        {
            if (!await _unitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            var points =
                await _unitOfWork.BehaviourIncidents.GetBehaviourIncidentPointsCountByStudent(studentId,
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
                await _unitOfWork.BehaviourAchievementTypes.GetAllRecordedBehaviourAchievementTypes(academicYearId);

            return recordedAchievementTypes.Select(achievementType => new ChartDataCategoric(achievementType.Description, achievementType.Achievements.Count)).ToList();
        }

        public async Task<IEnumerable<ChartDataCategoric>> GetChartDataBehaviourIncidentsByType(int academicYearId)
        {
            var recordedBehaviourTypes =
                await _unitOfWork.BehaviourIncidentTypes.GetAllRecordedAchievementTypes(academicYearId);

            return recordedBehaviourTypes.Select(behaviourType => new ChartDataCategoric(behaviourType.Description, behaviourType.Incidents.Count)).ToList();
        }

        public async Task<int> GetTotalConductPointsByStudent(int studentId, int academicYearId)
        {
            if (!await _unitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            var achievementPoints =
                await _unitOfWork.BehaviourAchievements.GetAchievementPointsCountByStudent(studentId, academicYearId);

            var behaviourPoints =
                await _unitOfWork.BehaviourIncidents.GetBehaviourIncidentPointsCountByStudent(studentId,
                    academicYearId);

            return achievementPoints - behaviourPoints;
        }
        public async Task UpdateAchievement(BehaviourAchievement achievement)
        {
            var achievementInDb = await _unitOfWork.BehaviourAchievements.GetByIdAsync(achievement.Id);

            if (achievementInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Achievement not found");
            }

            achievementInDb.LocationId = achievement.LocationId;
            achievementInDb.Comments = achievement.Comments;
            achievementInDb.Points = achievement.Points;
            achievementInDb.Resolved = achievement.Resolved;
            achievementInDb.AchievementTypeId = achievement.AchievementTypeId;

            await _unitOfWork.Complete();
        }
        public async Task UpdateBehaviourIncident(BehaviourIncident incident)
        {
            var incidentInDb = await _unitOfWork.BehaviourIncidents.GetByIdAsync(incident.Id);

            if (incidentInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Incident not found");
            }

            incidentInDb.LocationId = incident.LocationId;
            incidentInDb.Comments = incident.Comments;
            incidentInDb.Points = incident.Points;
            incidentInDb.Resolved = incident.Resolved;
            incidentInDb.BehaviourTypeId = incident.BehaviourTypeId;

            await _unitOfWork.Complete();
        }

        public async Task<int> GetAchievementPointsToday()
        {
            return await _unitOfWork.BehaviourAchievements.GetBehaviourAchievementPointsToday();
        }

        public async Task<int> GetBehaviourPointsToday()
        {
            return await _unitOfWork.BehaviourIncidents.GetBehaviourIncidentPointsToday();
        }
    }
}