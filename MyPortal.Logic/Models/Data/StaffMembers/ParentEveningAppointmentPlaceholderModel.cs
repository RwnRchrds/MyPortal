using System;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.StaffMembers
{
    public class ParentEveningAppointmentPlaceholderModel
    {
        public ParentEveningAppointmentPlaceholderModel(Guid parentEveningId, Guid staffMemberId, DateTime start,
            DateTime end)
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