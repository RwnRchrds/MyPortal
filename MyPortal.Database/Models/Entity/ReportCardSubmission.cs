using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ReportCardSubmissions")]
    public class ReportCardSubmission : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ReportCardId { get; set; }

        [Column(Order = 2)]
        public Guid SubmittedById { get; set; }

        [Column(Order = 3)]
        public Guid WeekId { get; set; }

        [Column(Order = 4)]
        public Guid PeriodId { get; set; }

        [Column(Order = 5)]
        [StringLength(256)]
        public string Comments { get; set; }

        public virtual ReportCard ReportCard { get; set; }
        public virtual User SubmittedBy { get; set; }
        public virtual AttendanceWeek AttendanceWeek { get; set; }
        public virtual AttendancePeriod Period { get; set; }
        public virtual ICollection<ReportCardTargetSubmission> TargetSubmissions { get; set; }
    }
}
