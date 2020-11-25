using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("BehaviourStatus")]
    public class BehaviourStatus : LookupItem
    {
        [Column(Order = 3)]
        public bool Resolved { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
