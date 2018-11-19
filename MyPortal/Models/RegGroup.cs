using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyPortal.Models
{
    public class RegGroup
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RegGroup()
        {
            Students = new HashSet<Student>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required] [StringLength(10)] public string Name { get; set; }

        [Display(Name = "Tutor")] public int TutorId { get; set; }

        [Display(Name = "Year Group")] public int YearGroupId { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual YearGroup YearGroup { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}