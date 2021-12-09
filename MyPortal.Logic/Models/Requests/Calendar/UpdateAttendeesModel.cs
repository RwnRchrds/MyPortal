using System;

namespace MyPortal.Logic.Models.Requests.Calendar
{
    public class UpdateAttendeesModel
    {
        public Guid EventId { get; set; }

        public EventAttendeeModel[] Attendees { get; set; }
    }
}