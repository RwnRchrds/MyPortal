namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Grade assigned to results.
    /// </summary>
    
    public class AssessmentGradeDto
    {
        public int Id { get; set; }

        public int GradeSetId { get; set; }

        
        public string Code { get; set; }

        public int Value { get; set; }

        public virtual AssessmentGradeSetDto GradeSet { get; set; }
    }
}
