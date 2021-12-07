using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Response.Contacts
{
    public class ParentEveningAppointmentTemplateModel
    {
        public ParentEveningAppointmentTemplateModel(Guid parentEveningId, Guid staffMemberId, DateTime start, DateTime end)
        {
            ParentEveningId = parentEveningId;
            StaffMemberId = staffMemberId;
            StartTime = start;
            EndTime = end;
            Available = true;
        }

        public DateRange GetDateRange()
        {
            return new DateRange(StartTime, EndTime);
        }

        public Guid ParentEveningId { get; set; }
        public Guid StaffMemberId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Available { get; set; }
    }
}