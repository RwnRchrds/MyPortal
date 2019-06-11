namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A set of results awarded to students. Result sets usually represent a time-frame eg Spring Term.
    /// </summary>
    public partial class AssessmentResultSetDto
    {
        public int Id { get; set; }

        [Display(Name="Academic Year")]
        public int AcademicYearId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name="Is Current")]
        public bool IsCurrent { get; set; }

        public virtual CurriculumAcademicYearDto CurriculumAcademicYear { get; set; }
    }
}
