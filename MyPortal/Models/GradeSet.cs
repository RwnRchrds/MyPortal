using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyPortal.Models
{
    public class GradeSet
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GradeSet()
        {
            Grades = new HashSet<Grade>();
        }

        public int Id { get; set; }

        [Required] [StringLength(255)] public string Name { get; set; }

        public bool IsKs4 { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Grade> Grades { get; set; }
    }
}