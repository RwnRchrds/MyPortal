using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models.Entity
{
    [Table("GradeSets")]
    public class GradeSet : LookupItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GradeSet()
        {
            Grades = new HashSet<Grade>();
            Aspects = new HashSet<Aspect>();
        }

        [Column(Order = 3)]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Column(Order = 4)]
        public bool System { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aspect> Aspects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Grade> Grades { get; set; }

        public virtual ICollection<ExamQualificationLevel> ExamQualificationLevels { get; set; }
    }
}
