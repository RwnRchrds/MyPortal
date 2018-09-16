using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyPortal.Models
{
    [Table("TrainingCourses")]
    public class TrainingCourse
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrainingCourse()
        {
            TrainingCertificates = new HashSet<TrainingCertificate>();
        }

        [Display(Name = "ID")] 
        public int Id { get; set; }

        [Required]
        [StringLength(255)] 
        public string Code { get; set; }

        [Required]
        [StringLength(1000)] 
        public string Description { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainingCertificate> TrainingCertificates { get; set; }
    }
}