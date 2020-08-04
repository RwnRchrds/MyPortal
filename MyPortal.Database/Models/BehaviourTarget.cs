using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("BehaviourTargets")]
    public class BehaviourTarget : LookupItem
    {
        public virtual ICollection<ReportCardTarget> ReportCardLinks { get; set; }
    }
}
