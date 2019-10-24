using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Attributes;
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
        public async Task<IEnumerable<StudentAttendanceMarkCollection>> LoadRegister([FromUri] int weekId, [FromUri] int sessionId)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            try
            {
                return await AttendanceProcesses.GetRegisterMarks(academicYearId, weekId, sessionId, _context, false);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("TakeRegister")]
        [Route("marks/takeRegister/dataGrid/{weekId:int}/{sessionId:int}", Name = "ApiAttendanceLoadRegisterDataGrid")]
        public async Task<IHttpActionResult> LoadRegisterDataGrid([FromBody] DataManagerRequest dm, [FromUri] int weekId,
            [FromUri] int sessionId)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

            try
            {
                var marks = await AttendanceProcesses.GetRegisterMarks(academicYearId, weekId, sessionId, _context,
                    true);
                return PrepareDataGridObject(marks, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("TakeRegister")]
        [Route("marks/saveRegister", Name = "ApiAttendanceSaveRegisterMarks")]
        public async Task<IHttpActionResult> SaveRegisterMarks(DataGridUpdate<StudentAttendanceMarkCollection> register)
        {
            if (register.Changed != null)
            {
                await AttendanceProcesses.SaveRegisterMarks(register.Changed, _context);
            }

            return Json(new List<StudentAttendanceMarkCollection>());
        }
        
        [HttpGet]
        [RequiresPermission("ViewAttendance")]
        [Route("summary/raw/{studentId:int}", Name = "ApiAttendanceGetRawAttendanceSummary")]
        public async Task<AttendanceSummary> GetRawAttendanceSummary([FromUri] int studentId)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            
            await AuthenticateStudentRequest(studentId);

            try
            {
                return await AttendanceProcesses.GetSummary(studentId, academicYearId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewAttendance")]
        [Route("summary/percent/{studentId:int}", Name = "ApiAttendanceGetPercentageAttendanceSummary")]
        public async Task<AttendanceSummary> GetPercentageAttendanceSummary([FromUri] int studentId)
        {
            var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);
            
            await AuthenticateStudentRequest(studentId);

            try
            {
                return await AttendanceProcesses.GetSummary(studentId, academicYearId, _context, true);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [Route("periods/get/all", Name = "ApiAttendanceGetAllPeriods")]
        public async Task<IEnumerable<AttendancePeriodDto>> GetAllPeriods()
        {
            try
            {
                return await AttendanceProcesses.GetAllPeriods(_context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [Route("periods/get/byId/{periodId:int}", Name = "ApiAttendanceGetPeriodById")]
        public async Task<AttendancePeriodDto> GetPeriodById([FromUri] int periodId)
        {
            try
            {
                return await AttendanceProcesses.GetPeriodById(periodId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditAcademicYears")]
        [Route("weeks/createForYear/{academicYearId:int}", Name = "ApiAttendanceCreateAttendanceWeeksForAcademicYear")]
        public async Task<IHttpActionResult> CreateAttendanceWeeksForAcademicYear([FromUri] int academicYearId)
        {
            try
            {
                await AttendanceProcesses.CreateAttendanceWeeksForAcademicYear(academicYearId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Attendance weeks created");
        }

        [HttpGet]
        [Route("weeks/get/byDate/{date:datetime}", Name = "ApiAttendanceGetWeekByDate")]
        public async Task<AttendanceWeekDto> GetWeekByDate([FromUri] DateTime date)
        {
             var academicYearId = await SystemProcesses.GetCurrentOrSelectedAcademicYearId(_context, User);

             try
             {
                 return await AttendanceProcesses.GetWeekByDate(academicYearId, date, _context);
             }
             catch (Exception e)
             {
                 throw GetException(e);
             }
        }
    }
}
