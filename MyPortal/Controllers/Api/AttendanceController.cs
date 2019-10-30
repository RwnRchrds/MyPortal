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
using MyPortal.Services;
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
            var academicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(User);

            try
            {
                using (var service = new AttendanceService(UnitOfWork))
                {
                    return await service.GetRegisterMarks( weekId, sessionId);
                }
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
            var academicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(UnitOfWork, User);

            try
            {
                using (var service = new AttendanceService(UnitOfWork))
                {
                    var marks = await service.GetRegisterMarks(weekId, sessionId);
                    return PrepareDataGridObject(marks, dm);
                }
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
                try
                {
                    using (var service = new AttendanceService(UnitOfWork))
                    {
                        await service.SaveRegisterMarks(register.Changed);
                    }
                }
                catch (Exception e)
                {
                    return HandleException(e);
                }
            }

            return Json(new List<StudentAttendanceMarkCollection>());
        }
        
        [HttpGet]
        [RequiresPermission("ViewAttendance")]
        [Route("summary/raw/{studentId:int}", Name = "ApiAttendanceGetRawAttendanceSummary")]
        public async Task<AttendanceSummary> GetRawAttendanceSummary([FromUri] int studentId)
        {
            var academicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(UnitOfWork, User);
            
            await AuthenticateStudentRequest(studentId);

            try
            {
                using (var service = new AttendanceService(UnitOfWork))
                {
                    return await service.GetSummary(studentId, academicYearId);
                }
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
            var academicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(UnitOfWork, User);
            
            await AuthenticateStudentRequest(studentId);

            try
            {
                using (var service = new AttendanceService(UnitOfWork))
                {
                    return await service.GetSummary(studentId, academicYearId, true);
                }
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
                using (var service = new AttendanceService(UnitOfWork))
                {
                    return await service.GetAllPeriods();
                }
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
                using (var service = new AttendanceService(UnitOfWork))
                {
                    return await service.GetPeriodById(periodId);
                }
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
                using (var service = new AttendanceService(UnitOfWork))
                {
                    await service.CreateAttendanceWeeksForAcademicYear(academicYearId);
                }
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
             var academicYearId = await SystemService.GetCurrentOrSelectedAcademicYearId(UnitOfWork, User);

             try
             {
                 using (var service = new AttendanceService(UnitOfWork))
                 {
                     return await service.GetWeekByDate(academicYearId, date);
                 }
             }
             catch (Exception e)
             {
                 throw GetException(e);
             }
        }
    }
}
