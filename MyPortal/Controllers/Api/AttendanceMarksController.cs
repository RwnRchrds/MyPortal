using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.LiteDtos;
using MyPortal.Dtos.ViewDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Processes;

namespace MyPortal.Controllers.Api
{
    [Authorize(Roles = ("Staff"))]
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
        [Route("api/attendance/marks/loadRegister/{weekId}/{classPeriodId}")]
        public IEnumerable<StudentRegisterMarksDto> LoadRegister(int weekId, int classPeriodId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(User);

            var attendanceWeek =
                _context.AttendanceWeeks.SingleOrDefault(x => x.Id == weekId && x.AcademicYearId == academicYearId);

            if (attendanceWeek == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var currentPeriod = _context.CurriculumClassPeriods.SingleOrDefault(x => x.Id == classPeriodId);

            if (currentPeriod == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var periodsInDay = _context.AttendancePeriods.Where(x => x.Weekday == currentPeriod.AttendancePeriod.Weekday).ToList();

            var markList = new List<StudentRegisterMarksDto>();

            foreach (var enrolment in currentPeriod.CurriculumClass.Enrolments)
            {
                var markObject = new StudentRegisterMarksDto();
                var student = enrolment.Student;
                markObject.Student = Mapper.Map<Student, StudentDto>(student);
                var marks = new List<AttendanceRegisterMark>();

                foreach (var period in periodsInDay)
                {
                    var mark = AttendanceProcesses.GetAttendanceMark(attendanceWeek, period, student);

                    marks.Add(mark);
                }

                var liteMarks = AttendanceProcesses.PrepareLiteMarkList(marks, true);

                markObject.Marks = liteMarks;
                markList.Add(markObject);
            }

            return markList.ToList().OrderBy(x => x.Student.LastName);
        }
    }
}