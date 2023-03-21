using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ReportCardTargets")]
    public class ReportCardTarget : BaseTypes.Entity
    {
        [Column(Order = 2)]
        public Guid ReportCardId { get; set; }

        [Column(Order = 3)]
        public Guid TargetId { get; set; }

        public virtual ReportCard ReportCard { get; set; }
        public virtual BehaviourTarget Target { get; set; }
        public virtual ICollection<ReportCardTargetEntry> Submissions { get; set; }
    }
}
