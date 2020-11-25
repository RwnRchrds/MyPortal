using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("TrainingCertificates")]
    public class TrainingCertificate : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid CourseId { get; set; }

        [Column(Order = 2)]
        public Guid StaffId { get; set; }

        [Column(Order = 3)]
        public Guid StatusId { get; set; }

        public virtual StaffMember StaffMember { get; set; }

        public virtual TrainingCourse TrainingCourse { get; set; }

        public virtual TrainingCertificateStatus Status { get; set; }
    }
}
