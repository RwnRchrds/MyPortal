using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Dtos.LiteDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;

namespace MyPortal.Processes
{
    public static class CurriculumProcesses
    {

        static CurriculumProcesses()
        {
         
        }

        public static bool StudentCanEnroll(MyPortalDbContext context, int studentId, int classId)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            var currClass = context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

            var alreadyEnrolled = context.CurriculumClassEnrolments.SingleOrDefault(x =>
                x.ClassId == classId && x.StudentId == studentId) != null;

            if (student == null)
            {
                throw new EntityNotFoundException("Student not found");
            }

            if (currClass == null)
            {
                throw new EntityNotFoundException("Class not found");
            }

            if (alreadyEnrolled)
            {
                throw new PersonNotFreeException(student.Person.LastName + ", " + student.Person.FirstName + " is already enrolled in " + currClass.Name);
            }

            var periods = GetPeriodsForClass(context, currClass.Id);

            foreach (var period in periods)
            {
                if (!PeriodIsFree(context, student, period.Id))
                {
                    throw new PersonNotFreeException(student.Person.LastName + ", " + student.Person.FirstName + " is not free during period " + period.Name);
                }
            }

            return true;
        }

        public static IEnumerable<AttendancePeriod> GetPeriodsForClass(MyPortalDbContext context, int classId)
        {
            var periods = new List<AttendancePeriod>();
            var currClass = context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

            if (currClass == null)
            {
                return null;
            }

            foreach (var period in currClass.CurriculumClassPeriods)
            {
                periods.Add(period.AttendancePeriod);
            }

            return periods;
        }

        public static bool PeriodIsFree(MyPortalDbContext context, Student student, int periodId)
        {
            var isFree = true;

            var enrolments = context.CurriculumClassEnrolments.Where(x => x.StudentId == student.Id).ToList();

            foreach (var enrolment in enrolments)
            {
                var periods = GetPeriodsForClass(context, enrolment.ClassId);

                if (periods.Any(x => x.Id == periodId))
                {
                    isFree = false;
                }
            }

            return isFree;
        }       
        
        #region Extension Methods
        public static bool HasPeriods(this CurriculumClass currClass)
        {
            return currClass.CurriculumClassPeriods.Any();
        }

        public static bool HasEnrolments(this CurriculumClass currClass)
        {
            return currClass.Enrolments.Any();
        }
        #endregion
    }
}