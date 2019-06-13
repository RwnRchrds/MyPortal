using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;

namespace MyPortal.Processes
{
    public static class BehaviourProcesses
    {
        public static int GetBehaviourPoints(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                throw new EntityNotFoundException("Student not found");
            }

            var achievements =
                context.BehaviourAchievements.Where(x =>
                    x.AcademicYearId == academicYearId && x.StudentId == studentId).ToList();

            var behaviourIncidents =
                context.BehaviourIncidents.Where(x => x.AcademicYearId == academicYearId && x.StudentId == studentId)
                    .ToList();

            var achievementPoints = achievements.Any() ? achievements.Sum(x => x.Points) : 0;
            var behaviourPoints = behaviourIncidents.Any() ? behaviourIncidents.Sum(x => x.Points) : 0;

            return achievementPoints - behaviourPoints;
        }

        public static int GetAchievementCount(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                throw new EntityNotFoundException("Student not found");
            }

            var posPoints =
                context.BehaviourAchievements.Count(x =>
                    x.AcademicYearId == academicYearId && x.StudentId == studentId);

            if (posPoints < 0)
            {
                throw new BadRequestException("Cannot have negative achievement count");
            }

            return posPoints;
        }

        public static int GetBehaviourCount(int studentId, int academicYearId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (student == null)
            {
                throw new EntityNotFoundException("Student not found");
            }

            var negPoints =
                context.BehaviourIncidents.Count(x =>
                    x.AcademicYearId == academicYearId && x.StudentId == studentId);

            if (negPoints < 0)
            {
                throw new BadRequestException("Cannot have negative achievement count");
            }

            return negPoints;
        }
    }
}