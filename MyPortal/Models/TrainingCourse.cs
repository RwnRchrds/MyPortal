using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    [Table("TrainingCourses")]
    public partial class TrainingCourse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrainingCourse()
        {
            TrainingCertificates = new HashSet<TrainingCertificate>();
        }

        [Display(Name = "ID")]
        public int Id { get; set; }

        [StringLength(255)]
        public string Code { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainingCertificate> TrainingCertificates { get; set; }
    }
}
