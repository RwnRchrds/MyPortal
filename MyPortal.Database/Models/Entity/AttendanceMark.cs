using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("AttendanceMarks")]
    public class AttendanceMark : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid StudentId { get; set; }

        [Column(Order = 2)]
        public Guid WeekId { get; set; }

        [Column(Order = 3)]
        public Guid PeriodId { get; set; }

        [Column(Order = 4)]
        public Guid CodeId { get; set; }
        
        [Column(Order = 5)]
        public Guid CreatedById { get; set; }

        [Column(Order = 6)]
        [StringLength(256)]
        public string Comments { get; set; }

        [Column(Order = 7)]
        public int MinutesLate { get; set; }

        public virtual AttendanceCode AttendanceCode { get; set; }

        public virtual AttendancePeriod AttendancePeriod { get; set; }

        public virtual Student Student { get; set; }

        public virtual AttendanceWeek Week { get; set; }
        
        public virtual User CreatedBy { get; set; }
    }
}
