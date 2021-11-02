using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Constants;
using MyPortal.Database.Models;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class CalendarService : BaseService, ICalendarService
    {
        public async Task<IEnumerable<DiaryEventTypeModel>> GetEventTypes(bool includeReserved = false)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var eventTypes = await unitOfWork.DiaryEventTypes.GetAll(includeReserved);

                return eventTypes.Select(t => new DiaryEventTypeModel(t)).ToList();
            }
        }

        public async Task<IEnumerable<CalendarEventModel>> GetCalendarEventsByStudent(Guid studentId, DateRange dateRange)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var student = await unitOfWork.Students.GetById(studentId);

                var eventTypes = (await unitOfWork.DiaryEventTypes.GetAll(true)).ToList();

                var coverEventType = eventTypes.FirstOrDefault(t => t.Id == EventTypes.Cover);

                if (coverEventType == null)
                {
                    throw new NotFoundException("Could not find cover event type.");
                }

                var lessonEventType = eventTypes.FirstOrDefault(t => t.Id == EventTypes.Lesson);

                if (lessonEventType == null)
                {
                    throw new NotFoundException("Could not find lesson event type.");
                }

                var events = (await unitOfWork.DiaryEvents.GetByPerson(dateRange.Start, dateRange.End, student.PersonId))
                    .Select(e => new DiaryEventModel(e)).ToList();

                var calendarEvents = events.Select(e => new CalendarEventModel(e)).ToList();

                var sessions = await unitOfWork.Sessions.GetMetadataByStudent(studentId, dateRange.Start, dateRange.End);

                calendarEvents.AddRange(sessions.Select(s =>
                    new CalendarEventModel(s, s.IsCover ? coverEventType.ColourCode : lessonEventType.ColourCode)));

                return calendarEvents;
            }
        }
    }
}