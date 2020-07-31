using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("Aspects")]
    public class Aspect : LookupItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aspect()
        {
            Results = new HashSet<Result>();
        }

        [Column(Order = 3)]
        public Guid TypeId { get; set; }

        [Column(Order = 4)]
        public Guid? GradeSetId { get; set; }

        [Column(Order = 5, TypeName = "decimal(10,2)")]
        public decimal? MaxMark { get; set; }

        [Column(Order = 6)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 7)]
        public bool StudentVisible { get; set; }

        public virtual AspectType Type { get; set; }

        public virtual GradeSet GradeSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }

        public virtual ICollection<ExamComponent> Components { get; set; }

        public virtual ICollection<ExamElement> ExamEntries { get; set; }
        public virtual ICollection<ExamElement> ExamResults { get; set; }
    }
}