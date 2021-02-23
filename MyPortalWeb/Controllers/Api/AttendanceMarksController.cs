using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces.Services;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/attendanceMarks")]
    public class AttendanceMarksController : BaseApiController
    {
        private readonly IAttendanceMarkService _attendanceMarkService;

        public AttendanceMarksController(IUserService userService, IAcademicYearService academicYearService, IRolePermissionsCache rolePermissionsCache, IAttendanceMarkService attendanceMarkService) : base(userService, academicYearService, rolePermissionsCache)
        {
            _attendanceMarkService = attendanceMarkService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("register/{attendanceWeekId}/{sessionId}")]
        public async Task<IActionResult> GetRegister([FromRoute] Guid attendanceWeekId, [FromRoute] Guid sessionId)
        {
            return await ProcessAsync(async () =>
            {
                var register = await _attendanceMarkService.GetRegisterBySession(attendanceWeekId, sessionId);

                return Ok(register);
            });
        }
    }
}
