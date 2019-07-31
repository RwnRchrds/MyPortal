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
    [RoutePrefix("api/attendance")]
    public class AttendanceController : MyPortalApiController
    {
        [HttpGet]
        [Route("marks/loadRegister/{weekId:int}/{classPeriodId:int}")]
        public IEnumerable<StudentRegisterMarksDto> LoadRegister([FromUri] int weekId, [FromUri] int classPeriodId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(
                AttendanceProcesses.GetMarksForRegister(academicYearId, weekId, classPeriodId, _context));
        }

        [HttpGet]
        [Route("summary/raw/{studentId:int}")]
        public AttendanceSummary GetRawAttendanceSummary([FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            
            return PrepareResponseObject(AttendanceProcesses.GetSummary(studentId, academicYearId, _context));
        }

        [HttpGet]
        [Route("summary/percent/{studentId:int}")]
        public AttendanceSummary GetPercentageAttendanceSummary([FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(AttendanceProcesses.GetSummary(studentId, academicYearId, _context, true));
        }

        [HttpGet]
        [Route("periods/get/all")]
        public IEnumerable<AttendancePeriodDto> GetAllPeriods()
        {
            return PrepareResponseObject(AttendanceProcesses.GetAllPeriods(_context));
        }

        [HttpGet]
        [Route("periods/get/byId/{periodId:int}")]
        public AttendancePeriodDto GetPeriod([FromUri] int periodId)
        {
            return PrepareResponseObject(AttendanceProcesses.GetPeriod(periodId, _context));
        }

        [HttpPost]
        [Route("weeks/createForYear/{academicYearId:int}")]
        public IHttpActionResult CreateWeeks([FromUri] int academicYearId)
        {
            return PrepareResponse(AttendanceProcesses.CreateAttendanceWeeksForYear(academicYearId, _context));
        }

        [HttpGet]
        [Route("weeks/get/byDate/{date:datetime}")]
        public AttendanceWeekDto GetWeekByDate([FromUri] DateTime date)
        {
             var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
             return PrepareResponseObject(AttendanceProcesses.GetWeekByDate(academicYearId, date, _context));
        }
    }
}
