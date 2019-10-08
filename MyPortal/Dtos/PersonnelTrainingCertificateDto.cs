using MyPortal.Models.Database;

namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public partial class PersonnelTrainingCertificateDto
    {
        
        
        
        public int CourseId { get; set; }

        
        
        
        public int StaffId { get; set; }

        public CertificateStatus Status { get; set; }

        public virtual StaffMemberDto StaffMember { get; set; }

        public virtual PersonnelTrainingCourseDto PersonnelTrainingCourse { get; set; }
    }
}
