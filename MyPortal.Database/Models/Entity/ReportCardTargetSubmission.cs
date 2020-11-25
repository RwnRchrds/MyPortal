using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ReportCardTargetSubmissions")]
    public class ReportCardTargetSubmission : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid SubmissionId { get; set; }

        [Column(Order = 2)]
        public Guid TargetId { get; set; }

        [Column(Order = 3)]
        public bool TargetCompleted { get; set; }

        public virtual ReportCardSubmission Submission { get; set; }
        public virtual ReportCardTarget Target { get; set; }
    }
}
