namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A class in which students are enrolled.
    /// </summary>
    
    public partial class CurriculumClassDto
    {
        public int Id { get; set; }

        public int AcademicYearId { get; set; }

        public int? SubjectId { get; set; }

        
        public string Name { get; set; }

        public int TeacherId { get; set; }

        public int? YearGroupId { get; set; }

        public virtual StaffMemberDto Teacher { get; set; }

        public virtual CurriculumAcademicYearDto CurriculumAcademicYear { get; set; }

        
        

        public virtual CurriculumSubjectDto CurriculumSubject { get; set; }

        public virtual PastoralYearGroupDto PastoralYearGroup { get; set; }

        
        
    }
}
