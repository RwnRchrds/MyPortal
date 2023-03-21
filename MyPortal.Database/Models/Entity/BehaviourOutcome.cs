using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("BehaviourOutcomes")]
    public class BehaviourOutcome : LookupItem, ISystemEntity
    {
        public BehaviourOutcome()
        {
            StudentIncidents = new HashSet<StudentIncident>();
        }

        [Column(Order = 4)]
        public bool System { get; set; }

        public virtual ICollection<StudentIncident> StudentIncidents { get; set; }
    }
}
