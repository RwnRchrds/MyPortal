namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents a student enrolled in a class.
    /// </summary>
    [Table("Curriculum_Enrolments")]
    public partial class CurriculumEnrolment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public virtual CurriculumClass Class { get; set; }

        public virtual Student Student { get; set; }
    }
}
