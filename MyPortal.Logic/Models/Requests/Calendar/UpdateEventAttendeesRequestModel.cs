using System;

namespace MyPortal.Logic.Models.Requests.Calendar
{
    public class UpdateEventAttendeesRequestModel
    {
        public Guid EventId { get; set; }

        public EventAttendeeRequestModel[] Attendees { get; set; }
    }
}