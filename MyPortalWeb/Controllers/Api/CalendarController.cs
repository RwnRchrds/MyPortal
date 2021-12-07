using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Requests.Calendar;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Microsoft.AspNetCore.Components.Route("api/calendar")]
    [Authorize]
    public class CalendarController : PersonalDataController
    {
        private ICalendarService _calendarService;

        public CalendarController(IStudentService studentService, IPersonService personService,
            IUserService userService, IRoleService roleService, ICalendarService calendarService)
            : base(studentService, personService, userService, roleService)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        [Route("person/{personId}")]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventModel>), 200)]
        public async Task<IActionResult> GetCalendarEventsByPerson([FromRoute] Guid personId,
            [FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            try
            {
                if (await AuthorisePerson(personId))
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
                        await _calendarService.GetCalendarEventsByPerson(personId, dateRange);

                    return Ok(events);
                }

                return Error(403, PermissionMessage);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("create")]
        [Permission(PermissionValue.SchoolEditSchoolDiary)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventModel model)
        {
            try
            {
                await _calendarService.CreateEvent(model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
