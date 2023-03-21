using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("ReportCardEntries")]
    public class ReportCardEntry : BaseTypes.Entity, ICreatable
    {
        [Column(Order = 2)]
        public Guid ReportCardId { get; set; }

        [Column(Order = 3)]
        public Guid CreatedById { get; set; }

        [Column(Order = 4)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 5)]
        public Guid WeekId { get; set; }

        [Column(Order = 6)]
        public Guid PeriodId { get; set; }

        [Column(Order = 7)]
        [StringLength(256)]
        public string Comments { get; set; }

        public virtual ReportCard ReportCard { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual AttendanceWeek AttendanceWeek { get; set; }
        public virtual AttendancePeriod Period { get; set; }
        public virtual ICollection<ReportCardTargetEntry> TargetSubmissions { get; set; }
    }
}
