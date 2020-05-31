using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("BehaviourOutcome")]
    public class BehaviourOutcome : LookupItem
    {
        public BehaviourOutcome()
        {
            Incidents = new HashSet<Incident>();
        }

            public bool System { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
