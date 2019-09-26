namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A result a student has achieved for a particular subject.
    /// </summary>
    [Table("Assessment_Results")]
    public partial class AssessmentResult
    {
        public int Id { get; set; }

        [Display(Name="Result Set")]
        public int ResultSetId { get; set; }

        [Display(Name="Student")]
        public int StudentId { get; set; }

        [Display(Name="Subject")]
        public int SubjectId { get; set; }

        public int GradeSetId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        public virtual AssessmentResultSet AssessmentResultSet { get; set; }

        public virtual Student Student { get; set; }

        public virtual AssessmentGradeSet AssessmentGradeSet { get; set; }

        public virtual CurriculumSubject Subject { get; set; }
    }
}
