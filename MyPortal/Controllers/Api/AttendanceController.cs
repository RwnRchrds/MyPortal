using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models.Attributes;
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
        [RequiresPermission("TakeRegister")]
        [Route("marks/takeRegister/{weekId:int}/{sessionId:int}", Name = "ApiAttendanceLoadRegister")]
        public IEnumerable<StudentAttendanceMarkSingular> LoadRegister([FromUri] int weekId, [FromUri] int sessionId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            return PrepareResponseObject(
                    AttendanceProcesses.GetRegisterMarks(academicYearId, weekId, sessionId, _context, false))
                .Select(Mapper.Map<StudentAttendanceMarkCollection, StudentAttendanceMarkSingular>);
        }

        [HttpPost]
        [RequiresPermission("TakeRegister")]
        [Route("marks/takeRegister/dataGrid/{weekId:int}/{sessionId:int}", Name = "ApiAttendanceLoadRegisterDataGrid")]
        public IHttpActionResult LoadRegisterDataGrid([FromBody] DataManagerRequest dm, [FromUri] int weekId,
            [FromUri] int sessionId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            var registerMarks =
                PrepareResponseObject(
                        AttendanceProcesses.GetRegisterMarks(academicYearId, weekId, sessionId, _context, false))
                    .Select(Mapper.Map<StudentAttendanceMarkCollection, StudentAttendanceMarkSingular>);

            return PrepareDataGridObject(registerMarks, dm);
        }

        [HttpPost]
        [RequiresPermission("TakeRegister")]
        [Route("marks/saveRegister", Name = "ApiAttendanceSaveRegisterMarks")]
        public IHttpActionResult SaveRegisterMarks(DataGridUpdate<StudentAttendanceMarkCollection> register)
        {
            if (register.Changed != null)
            {
                AttendanceProcesses.SaveRegisterMarks(register.Changed, _context);
            }

            return Json(new List<StudentAttendanceMarkSingular>());
        }
        
        [HttpGet]
        [RequiresPermission("ViewAttendance")]
        [Route("summary/raw/{studentId:int}", Name = "ApiAttendanceGetRawAttendanceSummary")]
        public AttendanceSummary GetRawAttendanceSummary([FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            
            AuthenticateStudentRequest(studentId);
            
            return PrepareResponseObject(AttendanceProcesses.GetSummary(studentId, academicYearId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewAttendance")]
        [Route("summary/percent/{studentId:int}", Name = "ApiAttendanceGetPercentageAttendanceSummary")]
        public AttendanceSummary GetPercentageAttendanceSummary([FromUri] int studentId)
        {
            var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            
            AuthenticateStudentRequest(studentId);

            return PrepareResponseObject(AttendanceProcesses.GetSummary(studentId, academicYearId, _context, true));
        }

        [HttpGet]
        [Route("periods/get/all", Name = "ApiAttendanceGetAllPeriods")]
        public IEnumerable<AttendancePeriodDto> GetAllPeriods()
        {
            return PrepareResponseObject(AttendanceProcesses.GetAllPeriods(_context));
        }

        [HttpGet]
        [Route("periods/get/byId/{periodId:int}", Name = "ApiAttendanceGetPeriodById")]
        public AttendancePeriodDto GetPeriodById([FromUri] int periodId)
        {
            return PrepareResponseObject(AttendanceProcesses.GetPeriodById(periodId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditAcademicYears")]
        [Route("weeks/createForYear/{academicYearId:int}", Name = "ApiAttendanceCreateAttendanceWeeksForAcademicYear")]
        public IHttpActionResult CreateAttendanceWeeksForAcademicYear([FromUri] int academicYearId)
        {
            return PrepareResponse(AttendanceProcesses.CreateAttendanceWeeksForAcademicYear(academicYearId, _context));
        }

        [HttpGet]
        [Route("weeks/get/byDate/{date:datetime}", Name = "ApiAttendanceGetWeekByDate")]
        public AttendanceWeekDto GetWeekByDate([FromUri] DateTime date)
        {
             var academicYearId = SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
             return PrepareResponseObject(AttendanceProcesses.GetWeekByDate(academicYearId, date, _context));
        }
    }
}
