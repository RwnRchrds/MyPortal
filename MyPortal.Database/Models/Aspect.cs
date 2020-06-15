using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("Aspect")]
    public class Aspect : LookupItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aspect()
        {
            Results = new HashSet<Result>();
        }

        [DataMember]
        public Guid TypeId { get; set; }

        [DataMember]
        public Guid? GradeSetId { get; set; }

        [DataMember]
        [Column(TypeName = "decimal(10,2)")]
        public decimal? MaxMark { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public virtual AspectType Type { get; set; }

        public virtual GradeSet GradeSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }
    }
}