using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("AspectTypes")]
    public class AspectType : LookupItem, IReadOnlyEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspectType()
        {
            Aspects = new HashSet<Aspect>();
        }


        public virtual ICollection<Aspect> Aspects { get; set; }
    }
}