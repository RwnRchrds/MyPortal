using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Permissions;
using MyPortal.Logic.Models.Requests.Calendar;
using MyPortal.Logic.Models.Structures;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    public class CalendarController : PersonalDataController
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(IUserService userService, IPersonService personService,
            IStudentService studentService, ICalendarService calendarService)
            : base(userService, personService, studentService)
        {
            _calendarService = calendarService;
        }

        private async Task<EventAccessModel> GetEventAccess(Guid eventId)
        {
            var response = new EventAccessModel();

            var userId = User.GetUserId();
            var diaryEvent = await _calendarService.GetEvent(eventId);

            if (diaryEvent.CreatedById == userId)
            {
                response.CanView = true;
                response.CanEdit = true;
            }

            if (diaryEvent.Public)
            {
                if (await User.HasPermission(UserService, PermissionRequirement.RequireAll,
                        PermissionValue.SchoolViewSchoolDiary))
                {
                    response.CanView = true;
                }

                if (await User.HasPermission(UserService, PermissionRequirement.RequireAll,
                        PermissionValue.SchoolEditSchoolDiary))
                {
                    response.CanEdit = true;
                }
            }

            var user = await GetLoggedInUser();
            if (user.PersonId.HasValue)
            {
                var attendee = await _calendarService.GetEventAttendee(eventId, user.PersonId.Value);

                if (attendee != null)
                {
                    response.CanView = true;
                    response.CanEdit = attendee.CanEdit;
                }
            }

            return response;
        }

        [HttpGet]
        [Route("api/people/{personId}/calendar")]
        [ProducesResponseType(typeof(IEnumerable<CalendarEventModel>), 200)]
        public async Task<IActionResult> GetCalendarEventsByPerson([FromRoute] Guid personId,
            [FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            try
            {
                if (await CanAccessPerson(personId))
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

                return PermissionError();
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
                if (model.IsPublic && !await User.HasPermission(UserService, PermissionRequirement.RequireAll,
                        PermissionValue.SchoolEditSchoolDiary))
                {
                    return Error(403, "You do not have permission to edit the school diary.");
                }

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
                var access = await GetEventAccess(eventId);

                if (access.CanEdit)
                {
                    await _calendarService.UpdateEvent(eventId, model);

                    return Ok();
                }

                return Error(403, "You do not have permission to edit this event.");
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
                var access = await GetEventAccess(eventId);

                if (access.CanEdit)
                {
                    await _calendarService.DeleteEvent(eventId);

                    return Ok();
                }

                return Error(403, "You do not have permission to delete this event.");
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
                var access = await GetEventAccess(eventId);

                if (access.CanEdit)
                {
                    await _calendarService.CreateOrUpdateEventAttendees(eventId, model);

                    return Ok();
                }

                return Error(403, "You do not have permission to edit this event.");
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
                var access = await GetEventAccess(eventId);

                if (access.CanEdit)
                {
                    await _calendarService.DeleteEventAttendee(eventId, personId);

                    return Ok();
                }

                return Error(403, "You do not have permission to edit this event.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}