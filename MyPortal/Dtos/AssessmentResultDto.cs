namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A result a student has achieved for a particular subject. Results are added to a result set.
    /// </summary>
    public partial class AssessmentResultDto
    {
        public int ResultSetId { get; set; }


        public int StudentId { get; set; }


        public int SubjectId { get; set; }

        public string Value { get; set; }

        public virtual AssessmentResultSetDto AssessmentResultSet { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual CurriculumSubjectDto CurriculumSubject { get; set; }
    }
}
