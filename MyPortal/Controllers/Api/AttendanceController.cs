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
        [HttpGet]
        [Route("api/attendance/marks/loadRegister/{weekId}/{classPeriodId}")]
        public IEnumerable<StudentRegisterMarksDto> LoadRegister(int weekId, int classPeriodId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(
                AttendanceProcesses.GetMarksForRegister(academicYearId, weekId, classPeriodId, _context));
        }

        [HttpGet]
        [Route("api/attendance/summary/raw/{studentId}")]
        public AttendanceSummary GetRawAttendanceSummary(int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(AttendanceProcesses.GetSummary(studentId, academicYearId, _context));
        }

        [HttpGet]
        [Route("api/attendance/summary/percent/{studentId}")]
        public AttendanceSummary GetPercentageAttendanceSummary(int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(AttendanceProcesses.GetSummary(studentId, academicYearId, _context, true));
        }

        [HttpGet]
        [Route("api/attendance/periods/get/all")]
        public IEnumerable<AttendancePeriodDto> GetAllPeriods()
        {
            return PrepareResponseObject(AttendanceProcesses.GetAllPeriods(_context));
        }

        [HttpGet]
        public AttendancePeriodDto GetPeriod(int periodId)
        {
            return PrepareResponseObject(AttendanceProcesses.GetPeriod(periodId, _context));
        }

        [HttpPost]
        [Route("api/attendance/weeks/createForYear/{academicYearId:int}")]
        public IHttpActionResult CreateWeeks([FromUri] int academicYearId)
        {
            return PrepareResponse(AttendanceProcesses.CreateAttendanceWeeksForYear(academicYearId, _context));
        }

        [HttpGet]
        [Route("api/attendance/weeks/get/byDate/{dateString}")]
        public AttendanceWeekDto GetWeekByDate(int dateString)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            var date = DateTimeProcesses.GetDateTimeFromFormattedInt(dateString).ResponseObject;

            return PrepareResponseObject(AttendanceProcesses.GetWeekByDate(academicYearId, date, _context));
        }
    }
}
