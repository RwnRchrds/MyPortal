using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Dtos.Lite;
using MyPortal.Models.Database;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/attendance")]
    [Authorize]
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
            if (attendanceMarks != null)
            {
                try
                {
                    await _service.SaveRegisterMarks(attendanceMarks, true);       
                }
                catch (Exception e)
                {
                    return HandleException(e);
                }
            }

            return Ok("Register saved");
        }
        
        [HttpGet]
        [RequiresPermission("ViewAttendance")]
        [Route("summary/raw/{studentId:int}", Name = "ApiGetRawAttendanceSummary")]
        public async Task<AttendanceSummary> GetRawAttendanceSummary([FromUri] int studentId)
        {
            await AuthenticateStudentRequest(studentId);

            try
            {
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);
                    
                    var summary = await _service.GetSummary(studentId, academicYearId);

                    return summary;
                }
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
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var summary = await _service.GetSummary(studentId, academicYearId, true);

                    return summary;
                }
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [Route("periods/get/all", Name = "ApiGetAllPeriods")]
        public async Task<IEnumerable<AttendancePeriodDto>> GetAllPeriods()
        {
            try
            {
                var periods = await _service.GetAllPeriods();

                return periods.Select(Mapper.Map<AttendancePeriod, AttendancePeriodDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [Route("periods/get/byId/{periodId:int}", Name = "ApiGetPeriodById")]
        public async Task<AttendancePeriodDto> GetPeriodById([FromUri] int periodId)
        {
            try
            {
                var period = await _service.GetPeriodById(periodId);

                return Mapper.Map<AttendancePeriod, AttendancePeriodDto>(period);
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
                using (var curriculumService = new CurriculumService(UnitOfWork))
                {
                    var academicYearId = await curriculumService.GetCurrentOrSelectedAcademicYearId(User);

                    var week = await _service.GetWeekByDate(academicYearId, date);

                    return Mapper.Map<AttendanceWeek, AttendanceWeekDto>(week);
                }
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
    }
}
