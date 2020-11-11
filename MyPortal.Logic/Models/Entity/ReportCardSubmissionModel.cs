using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ReportCardSubmissionModel : BaseModel
    {
        public Guid ReportCardId { get; set; }
        public Guid SubmittedById { get; set; }
        public Guid WeekId { get; set; }
        public Guid PeriodId { get; set; }

        [StringLength(256)]
        public string Comments { get; set; }

        public virtual ReportCardModel ReportCard { get; set; }
        public virtual UserModel SubmittedBy { get; set; }
        public virtual AttendanceWeekModel AttendanceWeek { get; set; }
        public virtual AttendancePeriodModel Period { get; set; }
    }
}
