namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attendance_RegisterMarks")]
    public partial class AttendanceRegisterMark
    {
        public int Id { get; set; }

        [Display(Name="Student")]
        public int StudentId { get; set; }

        [Display(Name="Attendance Week")]
        public int WeekId { get; set; }

        [Display(Name="Attendance Period")]
        public int PeriodId { get; set; }

        [Required]
        [StringLength(1)]
        public string Mark { get; set; }

        public virtual AttendancePeriod AttendancePeriod { get; set; }

        public virtual Student Student { get; set; }

        public virtual AttendanceWeek AttendanceWeek { get; set; }
    }
}
