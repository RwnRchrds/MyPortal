using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("ReportCardSubmissions")]
    public class ReportCardSubmission : Entity
    {
        [Column(Order = 1)]
        public Guid ReportCardId { get; set; }

        [Column(Order = 2)]
        public Guid SubmittedById { get; set; }

        [Column(Order = 3)]
        public DateTime SubmissionDate { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string Comments { get; set; }

        public virtual ReportCard ReportCard { get; set; }
        public virtual ApplicationUser SubmittedBy { get; set; }
        public virtual ICollection<ReportCardTargetSubmission> TargetSubmissions { get; set; }
    }
}
