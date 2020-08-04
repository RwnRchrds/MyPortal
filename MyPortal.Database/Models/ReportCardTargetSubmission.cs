using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ReportCardTargetSubmissions")]
    public class ReportCardTargetSubmission : Entity
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
