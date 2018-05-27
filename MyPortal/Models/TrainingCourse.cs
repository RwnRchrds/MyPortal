namespace MyPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
        [Display(Name = "Course Code")]
        public string Code { get; set; }

        [StringLength(1000)]
        [Display(Name = "Course Description")]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainingCertificate> TrainingCertificates { get; set; }
    }
}
