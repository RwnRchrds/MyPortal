using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("TrainingCourses")]
    public class TrainingCourse : BaseTypes.LookupItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrainingCourse()
        {
            Certificates = new HashSet<TrainingCertificate>();
        }

        [Column(Order = 4)]
        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        [Column(Order = 5)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }


        public virtual ICollection<TrainingCertificate> Certificates { get; set; }
    }
}