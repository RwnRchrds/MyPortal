using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ParentEveningStaffMembers")]
    public class ParentEveningStaffMember : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ParentEveningId { get; set; }

        [Column(Order = 2)]
        public Guid StaffMemberId { get; set; }

        [Column(Order = 3)]
        public DateTime AvailableFrom { get; set; }

        [Column(Order = 4)]
        public DateTime AvailableTo { get; set; }

        [Column(Order = 5)]
        public int AppointmentLength { get; set; }

        [Column(Order = 6)]
        public int BreakLimit { get; set; }

        public virtual ParentEvening ParentEvening { get; set; }
        public virtual StaffMember StaffMember { get; set; }
        public virtual ICollection<ParentEveningAppointment> Appointments { get; set; }
        public virtual ICollection<ParentEveningBreak> Breaks { get; set; }
    }
}
