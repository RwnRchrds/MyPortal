using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    // FIXED
    [Table("ExclusionReasons")]
    public class ExclusionReason : LookupItem, ISystemEntity
    {
        public ExclusionReason()
        {
            Exclusions = new HashSet<Exclusion>();
        }

        [Column(Order = 3)]
        public bool System { get; set; }

        public virtual ICollection<Exclusion> Exclusions { get; set; }
    }
}
