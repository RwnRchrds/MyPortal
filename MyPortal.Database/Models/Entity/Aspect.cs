using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("Aspects")]
    public class Aspect : LookupItem, ISystemEntity
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
        public decimal? MinMark { get; set; }

        [Column(Order = 6, TypeName = "decimal(10,2)")]
        public decimal? MaxMark { get; set; }

        [Column(Order = 6)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 7)]
        [Required]
        [StringLength(50)]
        public string ColumnHeading { get; set; }

        [Column(Order = 7)]
        public bool StaffOnly { get; set; }

        [Column(Order = 8)]
        public bool System { get; set; }

        public virtual AspectType Type { get; set; }

        public virtual GradeSet GradeSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }

        public virtual ICollection<ExamAssessmentAspect> AssessmentAspects { get; set; }
    }
}