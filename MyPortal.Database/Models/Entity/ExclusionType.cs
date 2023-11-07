using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    // FIXED
    [Table("ExclusionTypes")]
    public class ExclusionType : LookupItem, ICensusEntity, ISystemEntity
    {
        public ExclusionType()
        {
            Exclusions = new HashSet<Exclusion>();
        }

        [Column(Order = 4)] public string Code { get; set; }

        [Column(Order = 5)] public bool System { get; set; }

        public virtual ICollection<Exclusion> Exclusions { get; set; }
    }
}