using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ParentEvenings")]
    public class ParentEvening : Database.BaseTypes.Entity
    {
        public ParentEvening()
        {
            StaffMembers = new HashSet<ParentEveningStaffMember>();
        }

        [Column(Order = 1)]
        public Guid EventId { get; set; }
        
        [Column(Order = 2)]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 2)]
        public DateTime BookingOpened { get; set; }

        [Column(Order = 3)]
        public DateTime BookingClosed { get; set; } 

        public virtual DiaryEvent Event { get; set; }
        public virtual ICollection<ParentEveningStaffMember> StaffMembers { get; set; }
    }
}
