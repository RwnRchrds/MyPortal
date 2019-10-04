namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Grade assigned to results.
    /// </summary>
    [Table("Assessment_Grades")]
    public class AssessmentGrade
    {
        public int Id { get; set; }

        public int GradeSetId { get; set; }

        [Required]
        [Column("Grade")]
        [StringLength(255)]
        public string GradeCode { get; set; }

        public int Value { get; set; }

        public virtual AssessmentGradeSet AssessmentGradeSet { get; set; }
    }
}
