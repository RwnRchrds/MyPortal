using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class DiaryEventService : BaseService, IDiaryEventService
    {
        public DiaryEventService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<DiaryEventTypeModel>> GetEventTypes(bool includeReserved = false)
        {
            var eventTypes = await UnitOfWork.DiaryEventTypes.GetAll(includeReserved);

            return eventTypes.Select(BusinessMapper.Map<DiaryEventTypeModel>).ToList();
        }

        public async Task<IEnumerable<CalendarEventModel>> GetCalendarEventsByStudent(Guid studentId, DateRange dateRange)
        {
            var student = await UnitOfWork.Students.GetById(studentId);

            var eventTypes = (await UnitOfWork.DiaryEventTypes.GetAll(true)).ToList();

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

            var events = (await UnitOfWork.DiaryEvents.GetByPerson(dateRange.Start, dateRange.End, student.PersonId))
                .Select(BusinessMapper.Map<DiaryEventModel>).ToList();

            var calendarEvents = events.Select(e => new CalendarEventModel(e)).ToList();

            var sessions = await UnitOfWork.Sessions.GetMetadataByStudent(studentId, dateRange.Start, dateRange.End);

            calendarEvents.AddRange(sessions.Select(s =>
                new CalendarEventModel(s, s.IsCover ? coverEventType.ColourCode : lessonEventType.ColourCode)));

            return calendarEvents;
        }
    }
}