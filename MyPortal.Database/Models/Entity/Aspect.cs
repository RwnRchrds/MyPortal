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

        [Column(Order = 4)]
        public Guid TypeId { get; set; }

        [Column(Order = 5)]
        public Guid? GradeSetId { get; set; }

        [Column(Order = 6, TypeName = "decimal(10,2)")]
        public decimal? MinMark { get; set; }

        [Column(Order = 7, TypeName = "decimal(10,2)")]
        public decimal? MaxMark { get; set; }

        [Column(Order = 8)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 9)]
        [Required]
        [StringLength(50)]
        public string ColumnHeading { get; set; }

        // Only visible to staff users
        [Column(Order = 10)]
        public bool Private { get; set; }

        [Column(Order = 11)]
        public bool System { get; set; }

        public virtual AspectType Type { get; set; }

        public virtual GradeSet GradeSet { get; set; }

        
        public virtual ICollection<Result> Results { get; set; }

        public virtual ICollection<ExamAssessmentAspect> AssessmentAspects { get; set; }
    }
}