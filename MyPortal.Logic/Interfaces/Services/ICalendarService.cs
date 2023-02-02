using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.Calendar;

using MyPortal.Logic.Models.Requests.Calendar;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ICalendarService
    {
        Task<IEnumerable<DiaryEventTypeModel>> GetEventTypes(bool includeReserved = false);

        Task<IEnumerable<CalendarEventModel>> GetCalendarEventsByPerson(Guid personId, DateTime dateFrom,
            DateTime dateTo, bool includeDeclined, bool includePrivate, bool hidePrivateDetails);

        Task CreateEvent(EventRequestModel model);

        Task UpdateEvent(Guid eventId, EventRequestModel model);

        Task DeleteEvent(Guid eventId);

        Task CreateOrUpdateEventAttendees(Guid eventId, EventAttendeesRequestModel model);

        Task DeleteEventAttendee(Guid eventId, Guid personId);
    }
}