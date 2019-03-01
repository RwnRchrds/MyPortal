using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace MyPortal.Models
{
    [Table("Assessment_GradeSets")]
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

        public bool Active { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Grade> Grades { get; set; }
    }
}