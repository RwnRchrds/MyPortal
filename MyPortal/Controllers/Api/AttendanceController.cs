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
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/attendance")]
    [Authorize]
    public class AttendanceController : MyPortalApiController
    {
        [HttpGet]
        [Route("marks/loadRegister/{weekId:int}/{sessionId:int}")]
        public IEnumerable<StudentLiteMarksCollection> LoadRegister([FromUri] int weekId, [FromUri] int sessionId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(
                AttendanceProcesses.GetMarksForRegister(academicYearId, weekId, sessionId, _context));
        }

        [HttpPost]
        [Route("marks/loadRegister/dataGrid/{weekId:int}/{sessionId:int}")]
        public IHttpActionResult LoadRegisterForDataGrid([FromBody] DataManagerRequest dm, [FromUri] int weekId,
            [FromUri] int sessionId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            var registerMarks =
                PrepareResponseObject(
                    AttendanceProcesses.GetMarksForRegister(academicYearId, weekId, sessionId, _context));

            return PrepareDataGridObject(registerMarks, dm);
        }

        [HttpPost]
        [Route("marks/saveRegister")]
        public IHttpActionResult SaveRegisterMarks(List<StudentLiteMarksCollection> changed, int? key)
        {
            var result = AttendanceProcesses.SaveRegisterMarks(changed, _context);

            return PrepareResponse(result);
        }
        
        [HttpGet]
        [Route("summary/raw/{studentId:int}")]
        public AttendanceSummary GetRawAttendanceSummary([FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            
            AuthenticateStudentRequest(studentId);
            
            return PrepareResponseObject(AttendanceProcesses.GetSummary(studentId, academicYearId, _context));
        }

        [HttpGet]
        [Route("summary/percent/{studentId:int}")]
        public AttendanceSummary GetPercentageAttendanceSummary([FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            
            AuthenticateStudentRequest(studentId);

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
