using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class ClassDto
    {
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        public Guid? SubjectId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public Guid TeacherId { get; set; }

        public Guid? YearGroupId { get; set; }

        public virtual StaffMemberDto Teacher { get; set; }

        public virtual AcademicYearDto AcademicYear { get; set; }

        public virtual SubjectDto Subject { get; set; }

        public virtual YearGroupDto YearGroup { get; set; }
    }
}
