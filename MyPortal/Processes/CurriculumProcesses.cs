using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;

namespace MyPortal.Processes
{
    public static class CurriculumProcesses
    {
        private static readonly MyPortalDbContext _context;

        static CurriculumProcesses()
        {
            _context = new MyPortalDbContext();           
        }

        public static void EnrolInClass(int studentId, int classId)
        {
            var student = _context.CoreStudents.SingleOrDefault(x => x.Id == studentId);

            var currClass = _context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

            if (student == null)
            {
                throw new EntityNotFoundException("Student not found");
            }

            if (currClass == null)
            {
                throw new EntityNotFoundException("Class not found");
            }

            var periods = GetPeriodsForClass(currClass.Id);

            foreach (var period in periods)
            {
                if (!PeriodIsFree(student, period.Id))
                {
                    throw new BadRequestException("Student is not free during period " + period.Name);
                }
            }

           var enrolment = new CurriculumClassEnrolment
           {
               StudentId = studentId,
               ClassId = classId
           };

           _context.CurriculumClassEnrolments.Add(enrolment);

           _context.SaveChanges();
        }

        public static IEnumerable<AttendancePeriod> GetPeriodsForClass(int classId)
        {
            var periods = new List<AttendancePeriod>();
            var currClass = _context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

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

        public static bool PeriodIsFree(Student student, int periodId)
        {
            var isFree = true;

            var enrolments = _context.CurriculumClassEnrolments.Where(x => x.StudentId == student.Id).ToList();

            foreach (var enrolment in enrolments)
            {
                var periods = GetPeriodsForClass(enrolment.ClassId);

                if (periods.Any(x => x.Id == periodId))
                {
                    isFree = false;
                }
            }

            return isFree;
        }
        
        #region Extension Methods
        public static bool CheckCanBeDeleted(this CurriculumClass currClass)
        {
            return !currClass.CurriculumClassPeriods.Any() && !currClass.Enrolments.Any();
        }
        #endregion
    }
}