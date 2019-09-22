using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class BehaviourProcesses
    {
        public static ProcessResponse<int> GetTotalConductPoints(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                return new ProcessResponse<int>(ResponseType.NotFound, "Student not found", 0);
            }

            var achievements =
                context.BehaviourAchievements.Where(x =>
                    x.AcademicYearId == academicYearId && x.StudentId == studentId).ToList();

            var behaviourIncidents =
                context.BehaviourIncidents.Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId)
                    .ToList();

            var achievementPoints = achievements.Any() ? achievements.Sum(x => x.Points) : 0;
            var behaviourPoints = behaviourIncidents.Any() ? behaviourIncidents.Sum(x => x.Points) : 0;

            return new ProcessResponse<int>(ResponseType.Ok, null, achievementPoints - behaviourPoints);
        }

        public static ProcessResponse<int> GetAchievementIncidentCount(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                return new ProcessResponse<int>(ResponseType.NotFound, "Student not found", 0);
            }

            var posPoints =
                context.BehaviourAchievements.Count(x =>
                    x.AcademicYearId == academicYearId && x.StudentId == studentId);

            if (posPoints < 0)
            {
                return new ProcessResponse<int>(ResponseType.BadRequest, "Cannot have negative incident count", 0);
            }

            return new ProcessResponse<int>(ResponseType.Ok, null, posPoints);
        }

        public static ProcessResponse<int> GetBehaviourIncidentCount(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                return new ProcessResponse<int>(ResponseType.NotFound, "Student not found", 0);
            }

            var negPoints =
                context.BehaviourIncidents.Count(x =>
                    x.AcademicYearId == academicYearId && x.StudentId == studentId);

            if (negPoints < 0)
            {
                return new ProcessResponse<int>(ResponseType.BadRequest, "Cannot have negative incident count", 0);
            }

            return new ProcessResponse<int>(ResponseType.Ok, null, negPoints);
        }

        public static ProcessResponse<int> GetAchievementPointsCount(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                return new ProcessResponse<int>(ResponseType.NotFound, "Student not found", 0);
            }

            var points = 0;

            var list =
                context.BehaviourAchievements.Where(x =>
                    x.AcademicYearId == academicYearId && x.StudentId == studentId && !x.Deleted).ToList();

            if (list.Any())
            {
                points = list.Sum(x => x.Points);
            }

            if (points < 0)
            {
                return new ProcessResponse<int>(ResponseType.BadRequest, "Cannot have negative points count", 0);
            }

            return new ProcessResponse<int>(ResponseType.Ok, null, points);
        }

        public static ProcessResponse<int> GetBehaviourPointsCount(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                return new ProcessResponse<int>(ResponseType.NotFound, "Student not found", 0);
            }

            var points = 0;

            var list =
                context.BehaviourIncidents.Where(x =>
                    x.AcademicYearId == academicYearId && x.StudentId == studentId && !x.Deleted).ToList();

            if (list.Any())
            {
                points = list.Sum(x => x.Points);
            }

            if (points < 0)
            {
                return new ProcessResponse<int>(ResponseType.BadRequest, "Cannot have negative points count", 0);
            }

            return new ProcessResponse<int>(ResponseType.Ok, null, points);
        }

        public static ProcessResponse<IEnumerable<ChartDataCategoric>> GetChartData_BehaviourIncidentsByType(int academicYearId, MyPortalDbContext context)
        {
            var recordedBehaviourTypes = context.BehaviourIncidentTypes.Where(x => x.BehaviourIncidents.Any(i => i.AcademicYearId == academicYearId))
                .Include(x => x.BehaviourIncidents).ToList();
            var chartData = new List<ChartDataCategoric>();

            foreach (var behaviourType in recordedBehaviourTypes)
            {
                var dataPoint = new ChartDataCategoric(behaviourType.Description, behaviourType.BehaviourIncidents.Count);
                chartData.Add(dataPoint);
            }

            return new ProcessResponse<IEnumerable<ChartDataCategoric>>(ResponseType.Ok, null, chartData);
        }

        public static ProcessResponse<IEnumerable<ChartDataCategoric>> GetChartData_AchievementsByType(int academicYearId, MyPortalDbContext context)
        {
            var recordedAchievementTypes =
                context.BehaviourAchievementTypes.Where(x => x.BehaviourAchievements.Any(i => i.AcademicYearId == academicYearId)).ToList();
            var chartData = new List<ChartDataCategoric>();

            foreach (var achievementType in recordedAchievementTypes)
            {
                var dataPoint = new ChartDataCategoric(achievementType.Description, achievementType.BehaviourAchievements.Count);
                chartData.Add(dataPoint);
            }

            return new ProcessResponse<IEnumerable<ChartDataCategoric>>(ResponseType.Ok, null, chartData);
        }

        public static ProcessResponse<IEnumerable<GridBehaviourAchievementDto>> GetAchievementsForGrid(int studentId,
            int academicYearId, MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridBehaviourAchievementDto>>(ResponseType.Ok, null, context.BehaviourAchievements.Where(
                    x => x.AcademicYearId == academicYearId && x.StudentId == studentId && !x.Deleted).OrderByDescending(x => x.Date).ToList()
                .Select(Mapper.Map<BehaviourAchievement, GridBehaviourAchievementDto>));
        }

        public static ProcessResponse<BehaviourAchievementDto> GetAchievementById(int achievementId, MyPortalDbContext context)
        {
            var achievement = context.BehaviourAchievements.SingleOrDefault(x => x.Id == achievementId);

            if (achievement == null)
            {
                return new ProcessResponse<BehaviourAchievementDto>(ResponseType.NotFound, "Student not found", null);
            }

            return new ProcessResponse<BehaviourAchievementDto>(ResponseType.Ok, null,
                Mapper.Map<BehaviourAchievement, BehaviourAchievementDto>(achievement));
        }

        public static ProcessResponse<object> CreateAchievement(BehaviourAchievement achievement, MyPortalDbContext context)
        {
            achievement.Date = DateTime.Today;

            if (!ValidationProcesses.ModelIsValid(achievement))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            if (!achievement.Date.IsInAcademicYear(context, achievement.AcademicYearId).ResponseObject)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Date is not in academic year", null);
            }

            context.BehaviourAchievements.Add(achievement);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Achievement added", null);
        }

        public static ProcessResponse<object> UpdateAchievement(BehaviourAchievement achievement,
            MyPortalDbContext context)
        {
            var achievementInDb = context.BehaviourAchievements.SingleOrDefault(x => x.Id == achievement.Id);

            if (achievementInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Achievement not found", null);
            }

            achievementInDb.LocationId = achievement.LocationId;
            achievementInDb.Comments = achievement.Comments;
            achievementInDb.Points = achievement.Points;
            achievementInDb.Resolved = achievement.Resolved;
            achievementInDb.AchievementTypeId = achievement.AchievementTypeId;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Achievement updated", null);
        }

        public static ProcessResponse<object> DeleteAchievement(int achievementId, MyPortalDbContext context)
        {
            var achievement = context.BehaviourAchievements.SingleOrDefault(x => x.Id == achievementId);

            if (achievement == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Achievement not found", null);
            }

            achievement.Deleted = true; //Flag as deleted

            //context.BehaviourAchievements.Remove(achievement); //Enable this to delete from the database
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Achievement deleted", null);
        }

        public static ProcessResponse<IEnumerable<GridBehaviourIncidentDto>> GetBehaviourIncidentsForGrid(int studentId,
            int academicYearId, MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridBehaviourIncidentDto>>(ResponseType.Ok, null, context.BehaviourIncidents.Where(
                    x => x.AcademicYearId == academicYearId && x.StudentId == studentId && !x.Deleted)
                .OrderByDescending(x => x.Date).ToList().Select(Mapper.Map<BehaviourIncident, GridBehaviourIncidentDto>));
        }

        public static ProcessResponse<BehaviourIncidentDto> GetBehaviourIncident(int incidentId,
            MyPortalDbContext context)
        {
            var incident = context.BehaviourIncidents.SingleOrDefault(x => x.Id == incidentId);

            if (incident == null)
            {
                return new ProcessResponse<BehaviourIncidentDto>(ResponseType.NotFound, "Incident not found", null);
            }

            return new ProcessResponse<BehaviourIncidentDto>(ResponseType.Ok, null,
                Mapper.Map<BehaviourIncident, BehaviourIncidentDto>(incident));
        }

        public static ProcessResponse<object> CreateBehaviourIncident(BehaviourIncident incident,
            MyPortalDbContext context)
        {
            incident.Date = DateTime.Today;

            if (!ValidationProcesses.ModelIsValid(incident))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            if (!incident.Date.IsInAcademicYear(context, incident.AcademicYearId).ResponseObject)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Date is not in academic year", null);
            }

            context.BehaviourIncidents.Add(incident);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Incident added", null);
        }

        public static ProcessResponse<object> UpdateBehaviourIncident(BehaviourIncident incident,
            MyPortalDbContext context)
        {
            var incidentInDb = context.BehaviourIncidents.SingleOrDefault(x => x.Id == incident.Id);

            if (incidentInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Incident not found", null);
            }

            incidentInDb.LocationId = incident.LocationId;
            incidentInDb.Comments = incident.Comments;
            incidentInDb.Points = incident.Points;
            incidentInDb.Resolved = incident.Resolved;
            incidentInDb.BehaviourTypeId = incident.BehaviourTypeId;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Incident updated", null);
        }

        public static ProcessResponse<object> DeleteBehaviourIncident(int incidentId, MyPortalDbContext context)
        {
            var incident = context.BehaviourIncidents.SingleOrDefault(x => x.Id == incidentId);

            if (incident == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Incident not found", null);
            }

            incident.Deleted = true;

            //_context.BehaviourIncidents.Remove(incident);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Incident deleted", null);
        }
    }
}