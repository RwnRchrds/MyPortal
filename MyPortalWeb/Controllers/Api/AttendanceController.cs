using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Attendance.Register;
using MyPortal.Logic.Models.Requests.Attendance;
using MyPortal.Logic.Models.Summary;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/attendance")]
    public class AttendanceController : BaseApiController
    {
        private IAttendanceService _attendanceService;

        public AttendanceController(IUserService userService, IRoleService roleService,
            IAttendanceService attendanceService) : base(userService, roleService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost]
        [Route("marks")]
        [Permission(PermissionValue.AttendanceEditAttendanceMarks)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SaveAttendanceMarks([FromBody] AttendanceMarkSummaryModel[] marks)
        {
            try
            {
                await _attendanceService.UpdateAttendanceMarks(marks);
                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("registers")]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.AttendanceViewAttendanceMarks)]
        [ProducesResponseType(typeof(IEnumerable<AttendanceRegisterSummaryModel>), 200)]
        public async Task<IActionResult> GetRegisters([FromQuery] RegisterSearchRequestModel searchOptions)
        {
            try
            {
                var registers = await _attendanceService.GetRegisters(searchOptions);

                return Ok(registers);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("weeks/{attendanceWeekId}/registers/{sessionId}")]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.AttendanceViewAttendanceMarks)]
        [ProducesResponseType(typeof(AttendanceRegisterDataModel), 200)]
        public async Task<IActionResult> GetRegister([FromRoute] Guid attendanceWeekId, [FromRoute] Guid sessionId)
        {
            try
            {
                var register = await _attendanceService.GetRegisterBySession(attendanceWeekId, sessionId);

                return Ok(register);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
