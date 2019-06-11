namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A certificate awarded to personnel who have completed a training course.
    /// </summary>
    public partial class PersonnelTrainingCertificateDto
    {
        public int CourseId { get; set; }

        public int StaffId { get; set; }

        public int StatusId { get; set; }

        public virtual StaffMemberDto StaffMember { get; set; }

        public virtual PersonnelTrainingCourseDto PersonnelTrainingCourse { get; set; }

        public virtual PersonnelTrainingStatusDto PersonnelTrainingStatus { get; set; }
    }
}
