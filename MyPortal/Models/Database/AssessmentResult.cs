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
    public class AssessmentResult
    {
        public int Id { get; set; }

        public int ResultSetId { get; set; }

        public int StudentId { get; set; }

        public int AspectId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        public virtual AssessmentResultSet AssessmentResultSet { get; set; }

        public virtual AssessmentAspect Aspect { get; set; }

        public virtual Student Student { get; set; }
    }
}
