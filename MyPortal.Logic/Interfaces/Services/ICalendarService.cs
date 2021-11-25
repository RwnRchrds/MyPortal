using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ICalendarService
    {
        Task<IEnumerable<DiaryEventTypeModel>> GetEventTypes(bool includeReserved = false);

        Task<IEnumerable<CalendarEventModel>> GetCalendarEventsByPerson(Guid personId, DateRange dateRange);
    }
}