using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Calendar;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ICalendarService
    {
        Task<IEnumerable<DiaryEventTypeModel>> GetEventTypes(bool includeReserved = false);

        Task<IEnumerable<CalendarEventModel>> GetCalendarEventsByPerson(Guid personId, DateRange dateRange);

        Task CreateEvent(EventRequestModel model);

        Task UpdateEvent(Guid eventId, EventRequestModel model);

        Task DeleteEvent(Guid eventId);

        Task CreateOrUpdateEventAttendees(Guid eventId, EventAttendeesRequestModel[] models);
    }
}