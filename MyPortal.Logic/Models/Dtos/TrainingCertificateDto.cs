using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class TrainingCertificateDto
    {
        public int CourseId { get; set; }

        public int StaffId { get; set; }

        public int StatusId { get; set; }

        public virtual StaffMemberDto StaffMember { get; set; }

        public virtual TrainingCourseDto TrainingCourse { get; set; }

        public virtual TrainingCertificateStatusDto Status { get; set; }
    }
}
