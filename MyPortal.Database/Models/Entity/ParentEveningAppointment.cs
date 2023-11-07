using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ParentEveningAppointments")]
    public class ParentEveningAppointment : Database.BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid ParentEveningStaffId { get; set; }

        [Column(Order = 3)] public Guid StudentId { get; set; }

        [Column(Order = 4)] public DateTime Start { get; set; }

        [Column(Order = 5)] public DateTime End { get; set; }

        [Column(Order = 6)] public bool TeacherAccepted { get; set; }

        [Column(Order = 7)] public bool ParentAccepted { get; set; }

        [Column(Order = 8)] public bool? Attended { get; set; }

        public ParentEveningStaffMember ParentEveningStaffMember { get; set; }
        public Student Student { get; set; }
    }
}