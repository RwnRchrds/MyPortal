namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public enum CertificateStatus
    {
        Completed = 1,
        Pending = 2,
        Failed = 3
    }

    /// <summary>
    /// A certificate awarded to personnel who have completed a training course.
    /// </summary>
    [Table("Personnel_TrainingCertificates")]
    public partial class PersonnelTrainingCertificate
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StaffId { get; set; }

        public CertificateStatus StatusId { get; set; }

        public virtual StaffMember StaffMember { get; set; }

        public virtual PersonnelTrainingCourse PersonnelTrainingCourse { get; set; }
    }
}
