using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Response.Attendance;
using MyPortal.Logic.Models.Response.Attendance.Register;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/attendanceMarks")]
    public class AttendanceMarksController : BaseApiController
    {
        private IAttendanceService _attendanceService;

        public AttendanceMarksController(IUserService userService, IRoleService roleService,
            IAttendanceService attendanceService) : base(userService, roleService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        [Route("register/{attendanceWeekId}/{sessionId}")]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.AttendanceViewAttendanceMarks)]
        [ProducesResponseType(typeof(AttendanceRegisterModel), 200)]
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
