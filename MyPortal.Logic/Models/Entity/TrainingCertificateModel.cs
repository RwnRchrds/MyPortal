using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class TrainingCertificateModel : BaseModel
    {
        public Guid CourseId { get; set; }
        
        public Guid StaffId { get; set; }
        
        public Guid StatusId { get; set; }

        public virtual StaffMemberModel StaffMember { get; set; }

        public virtual TrainingCourseModel TrainingCourse { get; set; }

        public virtual TrainingCertificateStatusModel Status { get; set; }
    }
}