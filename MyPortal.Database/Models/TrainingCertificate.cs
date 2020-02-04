using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("TrainingCertificate")]
    public partial class TrainingCertificate
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int StaffId { get; set; }

        public int StatusId { get; set; }

        public virtual StaffMember StaffMember { get; set; }

        public virtual TrainingCourse TrainingCourse { get; set; }

        public virtual TrainingCertificateStatus Status { get; set; }
    }
}
