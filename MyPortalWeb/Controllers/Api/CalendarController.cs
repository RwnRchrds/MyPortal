using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Microsoft.AspNetCore.Components.Route("api/calendar")]
    [Authorize]
    public class CalendarController : BaseApiController
    {
        private ICalendarService _calendarService;

        public CalendarController(IUserService userService, IRoleService roleService, ICalendarService calendarService)
            : base(userService, roleService)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("student/{studentId}")]
        [Permission(PermissionValue.StudentViewStudentDetails)]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventModel>), 200)]
        public async Task<IActionResult> GetStudentCalendarEvents([FromRoute] Guid studentId,
            [FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            try
            {
                DateRange dateRange;

                if (dateFrom == null || dateTo == null)
                {
                    dateRange = DateRange.GetCurrentWeek();
                }
                else
                {
                    dateRange = new DateRange(dateFrom.Value, dateTo.Value);
                }

                var events =
                    await _calendarService.GetCalendarEventsByStudent(studentId, dateRange);

                return Ok(events);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
