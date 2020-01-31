using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class ClassDto
    {
        public int Id { get; set; }

        public int AcademicYearId { get; set; }

        public int? SubjectId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public int TeacherId { get; set; }

        public int? YearGroupId { get; set; }

        public virtual StaffMemberDto Teacher { get; set; }

        public virtual AcademicYearDto AcademicYear { get; set; }

        public virtual SubjectDto Subject { get; set; }

        public virtual YearGroupDto YearGroup { get; set; }
    }
}
