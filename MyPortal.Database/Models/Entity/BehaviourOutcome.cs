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
            Incidents = new HashSet<Incident>();
        }

        [Column(Order = 3)]
        public bool System { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
