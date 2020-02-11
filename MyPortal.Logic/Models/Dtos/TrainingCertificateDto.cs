using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class TrainingCertificateDto
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        public Guid StaffId { get; set; }

        public Guid StatusId { get; set; }

        public virtual StaffMemberDto StaffMember { get; set; }

        public virtual TrainingCourseDto TrainingCourse { get; set; }

        public virtual TrainingCertificateStatusDto Status { get; set; }
    }
}
