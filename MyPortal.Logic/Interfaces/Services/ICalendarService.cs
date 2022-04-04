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

        Task CreateEvent(Guid userId, params CreateEventModel[] models);

        Task UpdateEvent(params UpdateEventModel[] models);

        Task CreateOrUpdateEventAttendees(params UpdateAttendeesModel[] models);
    }
}