using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("TrainingCertificate")]
    public partial class TrainingCertificate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        public Guid StaffId { get; set; }

        public Guid StatusId { get; set; }

        public virtual StaffMember StaffMember { get; set; }

        public virtual TrainingCourse TrainingCourse { get; set; }

        public virtual TrainingCertificateStatus Status { get; set; }
    }
}
