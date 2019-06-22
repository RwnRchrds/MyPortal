using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.ViewDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Processes;

namespace MyPortal.Controllers.Api
{
    public class AttendanceController : MyPortalApiController
    {
        #region Marks
        [HttpGet]
        [Route("api/attendance/marks/loadRegister/{weekId}/{classPeriodId}")]
        public IEnumerable<StudentRegisterMarksDto> LoadRegister(int weekId, int classPeriodId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

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
                    var mark = AttendanceProcesses.GetAttendanceMark(_context, attendanceWeek, period, student);

                    marks.Add(mark);
                }

                var liteMarks = AttendanceProcesses.PrepareLiteMarkList(_context, marks, true);

                markObject.Marks = liteMarks;
                markList.Add(markObject);
            }

            return markList.ToList().OrderBy(x => x.Student.Person.LastName);
        }

        [HttpGet]
        [Route("api/attendance/summary/raw/{studentId}")]
        public AttendanceSummary GetRawAttendanceSummary(int studentId, int? academicYearId)
        {
            if (academicYearId == null)
            {
                academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            }

            var summary = AttendanceProcesses.GetSummary(_context, studentId, (int) academicYearId);

            return summary;
        }

        [HttpGet]
        [Route("api/attendance/summary/percent/{studentId}")]
        public AttendanceSummary GetPercentageAttendanceSummary(int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            var summary = AttendanceProcesses.GetSummary(_context, studentId, academicYearId, true);

            return summary;
        }

        #endregion

        #region Periods
        [HttpGet]
        [Route("api/attendance/periods/get/all")]
        public IEnumerable<AttendancePeriodDto> GetAllPeriods()
        {
            var dayIndex = new List<string> {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"};
            return _context.AttendancePeriods.ToList().OrderBy(x => dayIndex.IndexOf(x.Weekday))
                .ThenBy(x => x.StartTime).Select(Mapper.Map<AttendancePeriod, AttendancePeriodDto>);
        }

        [HttpGet]
        public AttendancePeriodDto GetPeriod(int periodId)
        {
            var period = _context.AttendancePeriods.SingleOrDefault(x => x.Id == periodId);

            if (period == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<AttendancePeriod, AttendancePeriodDto>(period);
        }

        #endregion Periods

        #region Weeks
        [HttpPost]
        [Route("api/attendance/weeks/createBulk")]
        public IHttpActionResult CreateWeeks(CurriculumAcademicYear academicYear)
        {
            var academicYearInDb = _context.CurriculumAcademicYears.SingleOrDefault(x => x.Id == academicYear.Id);

            if (academicYearInDb == null)
            {
                return Content(HttpStatusCode.BadRequest, "Academic Year Not Found");
            }

            var pointer = academicYear.FirstDate;
            while (pointer < academicYear.LastDate)
            {
                var weekBeginning = pointer.StartOfWeek();

                if (_context.AttendanceWeeks.Any(x => x.Beginning == weekBeginning))
                {
                    continue;
                }

                var attendanceWeek = new AttendanceWeek()
                {
                    AcademicYearId = academicYear.Id,
                    Beginning = weekBeginning
                };

                _context.AttendanceWeeks.Add(attendanceWeek);
                
                pointer = weekBeginning.AddDays(7);
            }

            _context.SaveChanges();

            return Ok("Attendance weeks created");
        }

        #endregion Weeks
    }
}
