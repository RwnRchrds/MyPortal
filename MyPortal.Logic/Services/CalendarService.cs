﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Constants;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Calendar;
using Task = System.Threading.Tasks.Task;

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
        
        public async Task<IEnumerable<CalendarEventModel>> GetCalendarEventsByPerson(Guid personId, DateRange dateRange)
        {
            var calendarEvents = new List<CalendarEventModel>();
            
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                // Get event type data so events can be coloured correctly
                var eventTypes = (await unitOfWork.DiaryEventTypes.GetAll(true)).ToList();
                
                // Verify person exists
                var person = await unitOfWork.People.GetPersonWithTypesById(personId);

                if (person == null)
                {
                    throw new NotFoundException("Person not found.");
                }
                
                // Get all generic events for person
                var events =
                    (await unitOfWork.DiaryEvents.GetByPerson(dateRange.Start, dateRange.End, personId, false,
                        true))
                    .Select(e => new DiaryEventModel(e)).ToList();

                foreach (var diaryEvent in events)
                {
                    calendarEvents.Add(new CalendarEventModel(diaryEvent));
                }

                // If person is student or staff, get lesson events
                if (person.PersonTypes.IsStudent || person.PersonTypes.IsStaff)
                {
                    IEnumerable<SessionMetadata> sessions = new List<SessionMetadata>();

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

                    if (person.PersonTypes.IsStudent)
                    {
                        var student = await unitOfWork.Students.GetByPersonId(personId);

                        if (student == null)
                        {
                            throw new NotFoundException("Student not found.");
                        }

                        sessions =
                            await unitOfWork.Sessions.GetMetadataByStudent(student.Id, dateRange.Start, dateRange.End);
                    }
                    else if (person.PersonTypes.IsStaff)
                    {
                        var staffMember = await unitOfWork.StaffMembers.GetByPersonId(personId);

                        if (staffMember == null)
                        {
                            throw new NotFoundException("Staff member not found.");
                        }

                        sessions = await unitOfWork.Sessions.GetMetadataByStaffMember(staffMember.Id, dateRange.Start,
                            dateRange.End);
                    }
                    
                    calendarEvents.AddRange(sessions.Select(s =>
                        new CalendarEventModel(s, s.IsCover ? coverEventType.ColourCode : lessonEventType.ColourCode)));
                }
                
                

                return calendarEvents;
            }
        }

        public async Task CreateEvent(Guid userId, params CreateEventModel[] models)
        {
            var eventTypes = (await GetEventTypes()).ToArray();

            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var user = await unitOfWork.Users.GetById(userId);
                
                foreach (var model in models)
                {
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
                        EventTypeId = model.EventTypeId,
                        RoomId = model.RoomId,
                        Subject = model.Subject,
                        Description = model.Description,
                        Location = model.Location,
                        StartTime = model.StartTime,
                        EndTime = model.EndTime,
                        IsPublic = model.IsPublic
                    };

                    if (user.PersonId.HasValue)
                    {
                        diaryEvent.Attendees.Add(new DiaryEventAttendee
                        {
                            PersonId = user.PersonId.Value,
                            Required = true,
                            ResponseId = AttendeeResponses.Accepted,
                            CanEdit = true
                        });
                    }

                    unitOfWork.DiaryEvents.Create(diaryEvent);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateEvent(params UpdateEventModel[] models)
        {
            var eventTypes = (await GetEventTypes()).ToArray();

            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var model in models)
                {
                    var eventInDb = await unitOfWork.DiaryEvents.GetById(model.Id);

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
                    eventInDb.IsPublic = model.IsPublic;

                    await unitOfWork.DiaryEvents.Update(eventInDb);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }
        
        public async Task CreateOrUpdateEventAttendees(params UpdateAttendeesModel[] models)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var model in models)
                {
                    var attendees = (await unitOfWork.DiaryEventAttendees.GetByEvent(model.EventId)).ToArray();

                    foreach (var attendee in model.Attendees)
                    {
                        var existingAttendee = attendees.FirstOrDefault(a => a.PersonId == attendee.PersonId);

                        if (existingAttendee != null)
                        {
                            existingAttendee.Required = attendee.Required;
                            existingAttendee.CanEdit = attendee.CanEdit;
                            existingAttendee.Attended = attendee.Attended;
                            existingAttendee.ResponseId = attendee.ResponseId;

                            await unitOfWork.DiaryEventAttendees.Update(existingAttendee);
                        }
                        else
                        {
                            var newAttendee = new DiaryEventAttendee
                            {
                                EventId = model.EventId,
                                PersonId = attendee.PersonId,
                                Required = attendee.Required,
                                CanEdit = attendee.CanEdit,
                                ResponseId = attendee.ResponseId,
                                Attended = attendee.Attended
                            };
                            
                            unitOfWork.DiaryEventAttendees.Create(newAttendee);
                        }
                    }
                }

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}