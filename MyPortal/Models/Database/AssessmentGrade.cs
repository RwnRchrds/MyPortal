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

        public int GradeSetId { get; set; }

        [Required]
        [StringLength(255)]
        public string Grade { get; set; }

        public virtual AssessmentGradeSet AssessmentGradeSet { get; set; }
    }
}
