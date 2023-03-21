using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ReportCardTargetEntries")]
    public class ReportCardTargetEntry : BaseTypes.Entity
    {
        [Column(Order = 2)]
        public Guid EntryId { get; set; }

        [Column(Order = 3)]
        public Guid TargetId { get; set; }

        [Column(Order = 4)]
        public bool TargetCompleted { get; set; }

        public virtual ReportCardEntry Entry { get; set; }
        public virtual ReportCardTarget Target { get; set; }
    }
}
