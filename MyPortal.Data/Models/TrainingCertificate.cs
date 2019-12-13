using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    public enum CertificateStatus
    {
        Completed = 1,
        Pending = 2,
        Failed = 3
    }

    /// <summary>
    /// A certificate awarded to personnel who have completed a training course.
    /// </summary>
    [Table("TrainingCertificate", Schema = "personnel")]
    public partial class TrainingCertificate
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StaffId { get; set; }

        public CertificateStatus Status { get; set; }

        public virtual StaffMember StaffMember { get; set; }

        public virtual TrainingCourse TrainingCourse { get; set; }
    }
}
