using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MyPortal.Attributes;
using MyPortal.Attributes.Filters;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.Lite;
using MyPortal.BusinessLogic.Models.Data;
using MyPortal.BusinessLogic.Services;
using MyPortal.BusinessLogic.Services.Identity;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/attendance")]
    [Authorize]
    [ValidateModel]
    public class AttendanceController : MyPortalApiController
    {
        private readonly AttendanceService _service;

        public AttendanceController()
        {
            _service = new AttendanceService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
        
        [HttpGet]
        [RequiresPermission("EditAttendance")]
        [Route("marks/TakeRegister/{weekId:int}/{sessionId:int}", Name = "ApiLoadRegister")]
        public async Task<IEnumerable<StudentAttendanceMarkCollection>> LoadRegister([FromUri] int weekId, [FromUri] int sessionId)
        {
            try
            {
                var registerMarks = await _service.GetRegisterMarks(weekId, sessionId);

                return registerMarks;
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditAttendance")]
        [Route("marks/saveRegister", Name = "ApiSaveRegisterMarks")]
        public async Task<IHttpActionResult> SaveRegisterMarks([FromBody] IEnumerable<AttendanceMarkLiteDto> attendanceMarks)
        {
            
                try
                {
                    if (attendanceMarks != null)
                    {
                        await _service.SaveRegisterMarks(attendanceMarks, true);
                        await _service.SaveChanges();
                    }

                    return Ok("Register saved.");
                }
                catch (Exception e)
                {
                    return HandleException(e);
                }
        }
        
        [HttpGet]
        [RequiresPermission("ViewAttendance")]
        [Route("summary/raw/{studentId:int}", Name = "ApiGetRawAttendanceSummary")]
        public async Task<AttendanceSummary> GetRawAttendanceSummary([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);

            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var summary = await _service.GetSummary(studentId, academicYearId);

                return summary;
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewAttendance")]
        [Route("summary/percent/{studentId:int}", Name = "ApiGetPercentageAttendanceSummary")]
        public async Task<AttendanceSummary> GetPercentageAttendanceSummary([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);
            
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                var summary = await _service.GetSummary(studentId, academicYearId, true);

                return summary;
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [Route("periods/get/all", Name = "ApiGetAllPeriods")]
        public async Task<IEnumerable<PeriodDto>> GetAllPeriods()
        {
            try
            {
                return await _service.GetAllPeriods();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [Route("periods/get/byId/{periodId:int}", Name = "ApiGetPeriodById")]
        public async Task<PeriodDto> GetPeriodById([FromUri] int periodId)
        {
            try
            {
                return await _service.GetPeriodById(periodId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditAcademicYears")]
        [Route("weeks/createForYear/{academicYearId:int}", Name = "ApiCreateAttendanceWeeksForAcademicYear")]
        public async Task<IHttpActionResult> CreateAttendanceWeeksForAcademicYear([FromUri] int academicYearId)
        {
            try
            {
                await _service.CreateAttendanceWeeksForAcademicYear(academicYearId);
                await _service.SaveChanges();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Attendance weeks created");
        }

        [HttpGet]
        [Route("weeks/get/byDate/{date:datetime}", Name = "ApiGetAttendanceWeekByDate")]
        public async Task<AttendanceWeekDto> GetAttendanceWeekByDate([FromUri] DateTime date)
        {
            try
            {
                var academicYearId = await User.GetSelectedOrCurrentAcademicYearId();

                return await _service.GetWeekByDate(academicYearId, date);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
    }
}
