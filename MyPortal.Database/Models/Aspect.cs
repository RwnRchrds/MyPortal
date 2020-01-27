using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("Aspect")]
    public class Aspect
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aspect()
        {
            Results = new HashSet<Result>();
        }

        public int Id { get; set; }

        public int TypeId { get; set; }

        public int GradeSetId { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual AspectType Type { get; set; }

        public virtual GradeSet GradeSet { get; set; }

        public bool Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }
    }
}