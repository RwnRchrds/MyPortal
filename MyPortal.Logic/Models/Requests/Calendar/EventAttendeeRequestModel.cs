using System;

namespace MyPortal.Logic.Models.Requests.Calendar
{
    public class EventAttendeeRequestModel
    {
        public Guid PersonId { get; set; }
        public bool Required { get; set; }
        public bool CanEdit { get; set; }
        public bool Attended { get; set; }
        public Guid? ResponseId { get; set; }
    }
}