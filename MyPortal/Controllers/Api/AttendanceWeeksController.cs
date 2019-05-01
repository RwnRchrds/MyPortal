using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Helpers;
using MyPortal.Models.Database;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    public class AttendanceWeeksController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public AttendanceWeeksController()
        {
            _context = new MyPortalDbContext();
        }

        public AttendanceWeeksController(MyPortalDbContext context)
        {
            _context = context;
        }

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

        [HttpGet]
        [Route("api/attendance/weeks/get/byDate/{dateString}")]
        public AttendanceWeekDto GetWeekByDate(int dateString)
        {
            var academicYearId = SystemHelper.GetCurrentOrSelectedAcademicYearId(User);
            int year = dateString / 10000;
            int month = ((dateString - (10000 * year)) / 100);
            int day = (dateString - (10000 * year) - (100 * month));

            var date = new DateTime(year, month, day);
            var weekBeginning = date.StartOfWeek();

            var selectedWeek = _context.AttendanceWeeks.SingleOrDefault(x => x.Beginning == weekBeginning && x.AcademicYearId == academicYearId);

            if (selectedWeek == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<AttendanceWeek, AttendanceWeekDto>(selectedWeek);
        }
    }
}
