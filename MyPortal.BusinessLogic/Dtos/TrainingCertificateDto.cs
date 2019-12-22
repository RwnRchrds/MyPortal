using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Dtos
{
    public class TrainingCertificateDto
    {
        public int CourseId { get; set; }

        public int StaffId { get; set; }

        public int StatusId { get; set; }

        public virtual TrainingCertificateStatus Status { get; set; }

        public virtual StaffMemberDto StaffMember { get; set; }

        public virtual TrainingCourseDto TrainingCourse { get; set; }

        public string GetStatus()
        {
            return Status.Description;
        }
    }
}
