using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Response.Contacts
{
    public class ParentEveningAppointmentTemplateModel
    {
        public ParentEveningAppointmentTemplateModel(Guid staffMemberId, DateTime start, DateTime end)
        {
            StaffMemberId = staffMemberId;
            StartTime = start;
            EndTime = end;
        }

        public DateRange GetDateRange()
        {
            return new DateRange(StartTime, EndTime);
        }
        
        public Guid StaffMemberId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Break { get; set; }
        public bool Occupied { get; set; }
    }
}