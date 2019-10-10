using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public static async Task CreateAchievement(BehaviourAchievement achievement, MyPortalDbContext context)
        {
            achievement.Date = DateTime.Today;

            if (!ValidationProcesses.ModelIsValid(achievement))
            {
                throw new BadRequestException("Invalid data");
            }

            if (!await context.CurriculumAcademicYears.AnyAsync(x => x.Id == achievement.AcademicYearId))
            {
                throw new NotFoundException("Academic year not found");
            }

            if (!achievement.Date.IsInAcademicYear(context, achievement.AcademicYearId).ResponseObject)
            {
                throw new BadRequestException("Date is not in academic year");
            }

            context.BehaviourAchievements.Add(achievement);
            await context.SaveChangesAsync();
        }

        public static async Task CreateBehaviourIncident(BehaviourIncident incident,
            MyPortalDbContext context)
        {
            incident.Date = DateTime.Today;

            if (!ValidationProcesses.ModelIsValid(incident))
            {
                throw new BadRequestException("Invalid data");
            }

            if (!await context.CurriculumAcademicYears.AnyAsync(x => x.Id == incident.AcademicYearId))
            {
                throw new NotFoundException("Academic year not found");
            }

            if (!incident.Date.IsInAcademicYear(context, incident.AcademicYearId).ResponseObject)
            {
                throw new BadRequestException("Date is not in academic year");
            }

            context.BehaviourIncidents.Add(incident);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteAchievement(int achievementId, MyPortalDbContext context)
        {
            var achievement = await context.BehaviourAchievements.SingleOrDefaultAsync(x => x.Id == achievementId);

            if (achievement == null)
            {
                throw new NotFoundException("Achievement not found");
            }

            achievement.Deleted = true; //Flag as deleted

            //context.BehaviourAchievements.Remove(achievement); //Enable this to delete from the database
            await context.SaveChangesAsync();
        }

        public static async Task DeleteBehaviourIncident(int incidentId, MyPortalDbContext context)
        {
            var incident = await context.BehaviourIncidents.SingleOrDefaultAsync(x => x.Id == incidentId);

            if (incident == null)
            {
                throw new NotFoundException("Incident not found");
            }

            incident.Deleted = true;

            //_context.BehaviourIncidents.Remove(incident);
            await context.SaveChangesAsync();
        }

        public static async Task<BehaviourAchievementDto> GetAchievementById(int achievementId, MyPortalDbContext context)
        {
            var achievement = await context.BehaviourAchievements.SingleOrDefaultAsync(x => x.Id == achievementId);

            if (achievement == null)
            {
                throw new NotFoundException("Achievement not found");
            }

            return Mapper.Map<BehaviourAchievement, BehaviourAchievementDto>(achievement);
        }

        public static async Task<int> GetAchievementCountByStudent(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }

            var achievementCount = await context.BehaviourAchievements.CountAsync(x =>
                x.AcademicYearId == academicYearId && x.StudentId == studentId);

            if (achievementCount < 0)
            {
                throw new BadRequestException("Cannot have negative achievement count");
            }

            return achievementCount;
        }

        public static async Task<int> GetAchievementPointsCountByStudent(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = await context.Students.SingleOrDefaultAsync(x => x.Id == studentId);

            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }

            var points =
                await context.BehaviourAchievements
                    .Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId && !x.Deleted)
                    .SumAsync(x => x.Points);

            if (points < 0)
            {
                throw new BadRequestException("Cannot have negative points count");
            }

            return points;
        }

        public static async Task<IEnumerable<GridBehaviourAchievementDto>> GetAchievementsForGrid(int studentId,
            int academicYearId, MyPortalDbContext context)
        {
            var achievements = await context.BehaviourAchievements
                .Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId && !x.Deleted)
                .OrderByDescending(x => x.Date).ToListAsync();

            return achievements.Select(Mapper.Map<BehaviourAchievement, GridBehaviourAchievementDto>);
        }

        public static async Task<BehaviourIncidentDto> GetBehaviourIncidentById(int incidentId,
            MyPortalDbContext context)
        {
            var incident = await context.BehaviourIncidents.SingleOrDefaultAsync(x => x.Id == incidentId);

            if (incident == null)
            {
                throw new NotFoundException("Incident not found");
            }

            return Mapper.Map<BehaviourIncident, BehaviourIncidentDto>(incident);
        }

        public static async Task<int> GetBehaviourIncidentCountByStudent(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }

            var negPoints =
                await context.BehaviourIncidents.CountAsync(x =>
                    x.AcademicYearId == academicYearId && x.StudentId == studentId);

            if (negPoints < 0)
            {
                throw new BadRequestException("Cannot have negative incident count");
            }

            return negPoints;
        }

        public static async Task<IEnumerable<GridBehaviourIncidentDto>> GetBehaviourIncidentsForGrid(int studentId,
            int academicYearId, MyPortalDbContext context)
        {
            var incidents = await context.BehaviourIncidents.Where(
                    x => x.AcademicYearId == academicYearId && x.StudentId == studentId && !x.Deleted)
                .OrderByDescending(x => x.Date).ToListAsync();

            return incidents.Select(Mapper.Map<BehaviourIncident, GridBehaviourIncidentDto>);
        }

        public static async Task<int> GetBehaviourPointsCountByStudent(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }

            var points = await context.BehaviourIncidents
                .Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId && !x.Deleted).SumAsync(x => x.Points);

            if (points < 0)
            {
                throw new BadRequestException("Cannot have negative points count");
            }

            return points;
        }

        public static async Task<IEnumerable<ChartDataCategoric>> GetChartDataAchievementsByType(int academicYearId, MyPortalDbContext context)
        {
            var recordedAchievementTypes = await context.BehaviourAchievementTypes
                .Where(x => x.Achievements.Any(i => i.AcademicYearId == academicYearId)).ToListAsync();

            return recordedAchievementTypes.Select(achievementType => new ChartDataCategoric(achievementType.Description, achievementType.Achievements.Count)).ToList();
        }

        public static async Task<IEnumerable<ChartDataCategoric>> GetChartDataBehaviourIncidentsByType(int academicYearId, MyPortalDbContext context)
        {
            var recordedBehaviourTypes = await context.BehaviourIncidentTypes
                .Where(x => x.Incidents.Any(i => i.AcademicYearId == academicYearId)).Include(x => x.Incidents)
                .ToListAsync();

            return recordedBehaviourTypes.Select(behaviourType => new ChartDataCategoric(behaviourType.Description, behaviourType.Incidents.Count)).ToList();
        }

        public static async Task<int> GetTotalConductPointsByStudent(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = await context.Students.SingleOrDefaultAsync(x => x.Id == studentId);

            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }

            var achievementPoints = await context.BehaviourAchievements
                .Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId).SumAsync(x => x.Points);

            var behaviourPoints = await context.BehaviourIncidents
                .Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId).SumAsync(x => x.Points);

            return achievementPoints - behaviourPoints;
        }
        public static async Task UpdateAchievement(BehaviourAchievement achievement,
            MyPortalDbContext context)
        {
            var achievementInDb = await context.BehaviourAchievements.SingleOrDefaultAsync(x => x.Id == achievement.Id);

            if (achievementInDb == null)
            {
                throw new NotFoundException("Achievement not found");
            }

            achievementInDb.LocationId = achievement.LocationId;
            achievementInDb.Comments = achievement.Comments;
            achievementInDb.Points = achievement.Points;
            achievementInDb.Resolved = achievement.Resolved;
            achievementInDb.AchievementTypeId = achievement.AchievementTypeId;

            await context.SaveChangesAsync();
        }
        public static async Task UpdateBehaviourIncident(BehaviourIncident incident,
            MyPortalDbContext context)
        {
            var incidentInDb = await context.BehaviourIncidents.SingleOrDefaultAsync(x => x.Id == incident.Id);

            if (incidentInDb == null)
            {
                throw new NotFoundException("Incident not found");
            }

            incidentInDb.LocationId = incident.LocationId;
            incidentInDb.Comments = incident.Comments;
            incidentInDb.Points = incident.Points;
            incidentInDb.Resolved = incident.Resolved;
            incidentInDb.BehaviourTypeId = incident.BehaviourTypeId;

            await context.SaveChangesAsync();
        }
    }
}