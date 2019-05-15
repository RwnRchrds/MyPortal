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

        [Display(Name="Student")]
        public int StudentId { get; set; }

        [Display(Name="Class")]
        public int ClassId { get; set; }

        public virtual CurriculumClass CurriculumClass { get; set; }

        public virtual Student Student { get; set; }
    }
}
