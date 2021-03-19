using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Caching;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Microsoft.AspNetCore.Components.Route("api/calendar")]
    [Authorize]
    public class CalendarController : BaseApiController
    {
        

        [HttpGet]
        [AllowAnonymous]
        [Route("student/{studentId}")]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventModel>), 200)]
        public async Task<IActionResult> GetStudentCalendarEvents([FromRoute] Guid studentId,
            [FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            return await ProcessAsync(async () =>
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
                    await Services.Calendar.GetCalendarEventsByStudent(studentId, dateRange);

                return Ok(events);
            });
        }

        public CalendarController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
        {
        }
    }
}
