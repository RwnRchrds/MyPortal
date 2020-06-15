using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("BehaviourStatus")]
    public class BehaviourStatus : LookupItem
    {
        [DataMember]
        public bool Resolved { get; set; }

        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
