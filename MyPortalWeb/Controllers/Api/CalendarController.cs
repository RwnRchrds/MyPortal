using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Requests.Calendar;
using MyPortal.Logic.Models.Structures;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    public class CalendarController : BaseApiController
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        [Route("api/people/{personId}/calendar")]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventModel>), 200)]
        public async Task<IActionResult> GetCalendarEventsByPerson([FromRoute] Guid personId,
            [FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            try
            {
                DateRange dateRange;

                if (dateFrom == null || dateTo == null)
                {
                    dateRange = DateRange.CurrentWeek;
                }
                else
                {
                    dateRange = new DateRange(dateFrom.Value, dateTo.Value);
                }

                var events =
                    await _calendarService.GetCalendarEventsByPerson(personId, dateRange.Start, dateRange.End);

                return Ok(events);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("events")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateEvent([FromBody] EventRequestModel model)
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

        [HttpPut]
        [Route("events/{eventId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateEvent([FromRoute] Guid eventId, [FromBody] EventRequestModel model)
        {
            try
            {
                await _calendarService.UpdateEvent(eventId, model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("events/{eventId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid eventId)
        {
            try
            {
                await _calendarService.DeleteEvent(eventId);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("events/{eventId}/attendees")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateOrUpdateAttendees([FromRoute] Guid eventId,
            [FromBody] EventAttendeesRequestModel model)
        {
            try
            {
                await _calendarService.CreateOrUpdateEventAttendees(eventId, model);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("events/{eventId}/attendees/{personId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteAttendee([FromRoute] Guid eventId, [FromRoute] Guid personId)
        {
            try
            {
                await _calendarService.DeleteEventAttendee(eventId, personId);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}