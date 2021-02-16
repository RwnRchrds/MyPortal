using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ParentEveningAppointments")]
    public class ParentEveningAppointment : Database.BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ParentEveningStaffId { get; set; }

        [Column(Order = 2)]
        public Guid StudentId { get; set; }

        [Column(Order = 3)]
        public DateTime Start { get; set; }

        [Column(Order = 4)]
        public DateTime End { get; set; }

        [Column(Order = 5)]
        public bool? Attended { get; set; }

        public ParentEveningStaffMember ParentEveningStaffMember { get; set; }
        public Student Student { get; set; }
    }
}
