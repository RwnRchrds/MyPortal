using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MyPortal.Helpers;
using MyPortal.Models.Database;

namespace MyPortal.Controllers.Api
{
    public class AttendanceMarksController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public AttendanceMarksController()
        {
            _context = new MyPortalDbContext();
        }

        public AttendanceMarksController(MyPortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/attendance/marks/loadRegister/{weekId}/{periodId}/{classId}")]
        public IEnumerable<AttendanceMark> LoadRegister(int weekId, int periodId, int classId)
        {
            var academicYearId = SystemHelper.GetCurrentOrSelectedAcademicYearId(User);

            var attendanceWeek =
                _context.AttendanceWeeks.SingleOrDefault(x => x.Id == weekId && x.AcademicYearId == academicYearId);

            if (attendanceWeek == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var currentPeriod = _context.AttendancePeriods.SingleOrDefault(x => x.Id == periodId);

            var periodsInDay = _context.AttendancePeriods.Where(x => x.Weekday == currentPeriod.Weekday).ToList();

            var registerClass =
                _context.CurriculumClasses.SingleOrDefault(x => x.AcademicYearId == academicYearId && x.Id == classId);

            if (registerClass == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var marks = new List<AttendanceMark>();

            foreach (var enrolment in registerClass.CurriculumClassEnrolments)
            {
                var student = enrolment.Student;

                foreach (var period in periodsInDay)
                {
                    var mark = new AttendanceMark
                    {
                        PeriodId = period.Id,
                        WeekId = attendanceWeek.Id,
                        StudentId = student.Id,
                        Mark = "-"
                    };

                    var markInDb = _context.AttendanceMarks.SingleOrDefault(x =>
                        x.PeriodId == periodId && x.WeekId == attendanceWeek.Id && x.StudentId == student.Id);

                    if (markInDb != null)
                    {
                        mark = markInDb;
                    }

                    marks.Add(mark);
                }
            }

            return marks;
        }
    }
}