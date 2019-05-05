namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Curriculum_ClassEnrolments")]
    public partial class CurriculumClassEnrolment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public virtual CurriculumClass CurriculumClass { get; set; }

        public virtual PeopleStudent Student { get; set; }
    }
}
