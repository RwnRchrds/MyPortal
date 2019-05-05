using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Helpers;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

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
        public IEnumerable<ListContainer<AttendanceMarkDto>> LoadRegister(int weekId, int periodId, int classId)
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

            var studentMarks = new List<ListContainer<AttendanceMarkDto>>();

            foreach (var enrolment in registerClass.CurriculumClassEnrolments)
            {
                var student = enrolment.Student;
                var marks = new ListContainer<AttendanceMarkDto>();
                var markList = new List<AttendanceMarkDto>();

                foreach (var period in periodsInDay)
                {
                    var mark = _context.AttendanceMarks.SingleOrDefault(x =>
                        x.PeriodId == periodId && x.WeekId == attendanceWeek.Id && x.StudentId == student.Id);
                    
                    if (mark == null)
                    {                        
                        mark = new AttendanceRegisterMark
                        {
                            PeopleStudent = student,
                            Mark = "-",
                            WeekId = weekId,
                            AttendanceWeek = attendanceWeek,
                            PeriodId = period.Id,
                            StudentId = student.Id,
                            AttendancePeriod = period
                        };
                    }
                    
                    markList.Add(Mapper.Map<AttendanceRegisterMark, AttendanceMarkDto>(mark));
                }

                marks.Objects = markList.OrderBy(x => x.AttendancePeriod.StartTime);
                studentMarks.Add(marks);

            }

            return studentMarks.ToList();
        }
    }
}