using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    public partial class RegGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RegGroup()
        {
            Students = new HashSet<Student>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(3)]
        [Display(Name = "Reg Tutor")]
        public string Tutor { get; set; }

        [Display(Name = "Year Group")]
        public int YearGroup { get; set; }

        public virtual Staff Staff { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
