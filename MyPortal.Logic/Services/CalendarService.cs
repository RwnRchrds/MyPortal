using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Curriculum;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Calendar;
using MyPortal.Logic.Models.Requests.Calendar;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class CalendarService : BaseService, ICalendarService
    {
        public CalendarService(ISessionUser user) : base(user)
        {
        }

        public async Task<IEnumerable<DiaryEventTypeModel>> GetEventTypes(bool includeReserved = false)
        {
            await using var unitOfWork = await User.GetConnection();

            var eventTypes = await unitOfWork.DiaryEventTypes.GetAll(includeReserved);

            return eventTypes.Select(t => new DiaryEventTypeModel(t)).ToList();
        }

        public async Task<IEnumerable<CalendarEventModel>> GetCalendarEventsByPerson(Guid personId, DateTime dateFrom,
            DateTime dateTo)
        {
            var calendarEvents = new List<CalendarEventModel>();

            await using var unitOfWork = await User.GetConnection();

            // Get event type data so events can be coloured correctly
            var eventTypes = (await unitOfWork.DiaryEventTypes.GetAll(true)).ToList();

            // Verify person exists
            var personService = new PersonService(User);
            var person = await personService.GetPersonWithTypesById(personId);

            var publicEvents = (await unitOfWork.DiaryEvents.GetPublicEvents(dateFrom, dateTo))
                .Select(e => new DiaryEventModel(e)).ToList();

            // Get all generic events for person
            var events =
                (await unitOfWork.DiaryEvents.GetByPerson(dateFrom, dateTo, personId))
                .Select(e => new DiaryEventModel(e)).ToList();

            foreach (var diaryEvent in events.Union(publicEvents))
            {
                calendarEvents.Add(new CalendarEventModel(diaryEvent));
            }

            // If person is student or staff, get lesson events
            if (person.PersonTypes.StudentId.HasValue || person.PersonTypes.StaffId.HasValue)
            {
                SessionPeriodDetailModel[] sessions = Array.Empty<SessionPeriodDetailModel>();

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

                if (person.PersonTypes.StudentId.HasValue)
                {
                    sessions =
                        (await unitOfWork.SessionPeriods.GetPeriodDetailsByStudent(person.PersonTypes.StudentId.Value,
                            dateFrom, dateTo)).ToArray();
                }
                else if (person.PersonTypes.StaffId.HasValue)
                {
                    sessions = (await unitOfWork.SessionPeriods.GetPeriodDetailsByStaffMember(
                        person.PersonTypes.StaffId.Value, dateFrom,
                        dateTo)).ToArray();
                }

                var sessionData = SessionHelper.GetSessionData(sessions);

                calendarEvents.AddRange(sessionData.Select(s =>
                    new CalendarEventModel(s, s.IsCover ? coverEventType.ColourCode : lessonEventType.ColourCode)));
            }

            return calendarEvents;
        }

        public async Task<DiaryEventModel> GetEvent(Guid eventId)
        {
            await using var unitOfWork = await User.GetConnection();

            var diaryEvent = new DiaryEventModel(await unitOfWork.DiaryEvents.GetById(eventId));

            return diaryEvent;
        }

        public async Task<IEnumerable<DiaryEventAttendeeModel>> GetAttendeesByEvent(Guid eventId)
        {
            await using var unitOfWork = await User.GetConnection();

            var attendees =
                (await unitOfWork.DiaryEventAttendees.GetByEvent(eventId)).Select(a =>
                    new DiaryEventAttendeeModel(a));

            return attendees;
        }

        public async Task<DiaryEventAttendeeModel> GetEventAttendee(Guid eventId, Guid personId)
        {
            await using var unitOfWork = await User.GetConnection();

            var attendee = await unitOfWork.DiaryEventAttendees.GetAttendee(eventId, personId);

            return new DiaryEventAttendeeModel(attendee);
        }

        public async Task CreateEvent(EventRequestModel model)
        {
            Validate(model);

            var userId = User.GetUserId();

            if (userId != null)
            {
                await using var unitOfWork = await User.GetConnection();

                var eventTypes = (await GetEventTypes()).ToArray();

                var user = await unitOfWork.Users.GetById(userId.Value);

                if (user == null)
                {
                    throw new EntityNotFoundException("User not found.");
                }

                var eventType = eventTypes.FirstOrDefault(t => t.Id == model.EventTypeId);

                if (eventType == null)
                {
                    throw new NotFoundException("Event type not found.");
                }

                if (eventType.System)
                {
                    throw new SystemEntityException("Events of this type cannot be created manually.");
                }

                var diaryEvent = new DiaryEvent
                {
                    Id = Guid.NewGuid(),
                    EventTypeId = model.EventTypeId,
                    RoomId = model.RoomId,
                    Subject = model.Subject,
                    Description = model.Description,
                    Location = model.Location,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Public = model.IsPublic,
                    AllDay = model.IsAllDay,
                    CreatedById = User.GetUserId()
                };

                if (model.IsAllDay)
                {
                    diaryEvent.StartTime = diaryEvent.StartTime.Date;
                    diaryEvent.EndTime = diaryEvent.EndTime.Date;
                }

                if (user.PersonId.HasValue)
                {
                    diaryEvent.Attendees.Add(new DiaryEventAttendee
                    {
                        Id = Guid.NewGuid(),
                        PersonId = user.PersonId.Value,
                        Required = true,
                        ResponseId = AttendeeResponses.Accepted,
                        CanEditEvent = true
                    });
                }

                unitOfWork.DiaryEvents.Create(diaryEvent);

                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw Unauthenticated();
            }
        }

        public async Task UpdateEvent(Guid eventId, EventRequestModel model)
        {
            Validate(model);

            await using var unitOfWork = await User.GetConnection();

            var eventTypes = (await GetEventTypes()).ToArray();

            var eventInDb = await unitOfWork.DiaryEvents.GetById(eventId);

            if (eventInDb.EventType.System)
            {
                throw new SystemEntityException("Events of this type cannot be updated manually.");
            }

            var eventType = eventTypes.FirstOrDefault(t => t.Id == model.EventTypeId);

            if (eventType == null)
            {
                throw new NotFoundException("Event type not found.");
            }

            eventInDb.EventTypeId = model.EventTypeId;
            eventInDb.RoomId = model.RoomId;
            eventInDb.Subject = model.Subject;
            eventInDb.Description = model.Description;
            eventInDb.Location = model.Location;
            eventInDb.StartTime = model.StartTime;
            eventInDb.EndTime = model.EndTime;
            eventInDb.Public = model.IsPublic;
            eventInDb.AllDay = model.IsAllDay;

            await unitOfWork.DiaryEvents.Update(eventInDb);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEvent(Guid eventId)
        {
            await using var unitOfWork = await User.GetConnection();

            await unitOfWork.DiaryEvents.Delete(eventId);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task CreateOrUpdateEventAttendees(Guid eventId, EventAttendeesRequestModel model)
        {
            await using var unitOfWork = await User.GetConnection();

            var attendees = (await unitOfWork.DiaryEventAttendees.GetByEvent(eventId)).ToArray();

            foreach (var attendee in model.Attendees)
            {
                Validate(attendee);

                var existingAttendee = attendees.FirstOrDefault(a => a.PersonId == attendee.PersonId);

                if (existingAttendee != null)
                {
                    existingAttendee.Required = attendee.Required;
                    existingAttendee.CanEditEvent = attendee.CanEdit;
                    existingAttendee.Attended = attendee.Attended;
                    existingAttendee.ResponseId = attendee.ResponseId;

                    await unitOfWork.DiaryEventAttendees.Update(existingAttendee);
                }
                else
                {
                    var newAttendee = new DiaryEventAttendee
                    {
                        Id = Guid.NewGuid(),
                        EventId = eventId,
                        PersonId = attendee.PersonId,
                        Required = attendee.Required,
                        CanEditEvent = attendee.CanEdit,
                        ResponseId = attendee.ResponseId,
                        Attended = attendee.Attended
                    };

                    unitOfWork.DiaryEventAttendees.Create(newAttendee);
                }
            }

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEventAttendee(Guid eventId, Guid personId)
        {
            await using var unitOfWork = await User.GetConnection();

            var attendees = await unitOfWork.DiaryEventAttendees.GetByEvent(eventId);

            foreach (var attendee in attendees.Where(a => a.PersonId == personId))
            {
                await unitOfWork.DiaryEventAttendees.Delete(attendee.Id);
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}