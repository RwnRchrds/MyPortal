using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ReportCardTargets")]
    public class ReportCardTarget : Entity
    {
        [Column(Order = 1)]
        public Guid ReportCardId { get; set; }

        [Column(Order = 2)]
        public Guid TargetId { get; set; }

        public virtual ReportCard ReportCard { get; set; }
        public virtual BehaviourTarget Target { get; set; }
        public virtual ICollection<ReportCardTargetSubmission> Submissions { get; set; }
    }
}
