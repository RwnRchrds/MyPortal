namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A training course available to staff.
    /// </summary>
    [Table("Personnel_TrainingCourses")]
    public partial class PersonnelTrainingCourse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonnelTrainingCourse()
        {
            Certificates = new HashSet<PersonnelTrainingCertificate>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonnelTrainingCertificate> Certificates { get; set; }
    }
}
