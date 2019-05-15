namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Assessment_Grades")]
    public partial class AssessmentGrade
    {
        public int Id { get; set; }

        [Display(Name="Grade Set")]
        public int GradeSetId { get; set; }

        [Required]
        [Display(Name="Grade")]
        [StringLength(255)]
        public string GradeValue { get; set; }

        public virtual AssessmentGradeSet AssessmentGradeSet { get; set; }
    }
}
