using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("NextOfKinRelationshipTypes")]
    public class NextOfKinRelationshipType : LookupItem, ISystemEntity
    {
        [Column(Order = 4)] public bool System { get; set; }

        public virtual ICollection<NextOfKin> NextOfKin { get; set; }
    }
}