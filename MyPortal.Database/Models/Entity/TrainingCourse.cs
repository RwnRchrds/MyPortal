using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("TrainingCourses")]
    public partial class TrainingCourse : BaseTypes.Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrainingCourse()
        {
            Certificates = new HashSet<TrainingCertificate>();
        }

        [Column(Order = 1)]
        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        [Column(Order = 2)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainingCertificate> Certificates { get; set; }
    }
}
