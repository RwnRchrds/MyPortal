namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Grades available to use in results.
    /// </summary>
    public partial class AssessmentGradeDto
    {
        public int Id { get; set; }
        public int GradeSetId { get; set; }
        public string GradeValue { get; set; }
        public virtual AssessmentGradeSetDto AssessmentGradeSet { get; set; }
    }
}
