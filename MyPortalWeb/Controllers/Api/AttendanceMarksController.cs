using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Response.Attendance;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/attendanceMarks")]
    public class AttendanceMarksController : BaseApiController
    {
        public AttendanceMarksController(IAppServiceCollection services) : base(services)
        {
        }

        [HttpGet]
        [Route("register/{attendanceWeekId}/{sessionId}")]
        [ProducesResponseType(typeof(AttendanceRegisterModel), 200)]
        public async Task<IActionResult> GetRegister([FromRoute] Guid attendanceWeekId, [FromRoute] Guid sessionId)
        {
            return await ProcessAsync(async () =>
            {
                var register = await Services.AttendanceMarks.GetRegisterBySession(attendanceWeekId, sessionId);

                return Ok(register);
            });
        }
    }
}
