namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// [NOT CURRENTLY IN USE] A generic template for lesson plan creation.
    /// </summary>
    [Table("Curriculum_LessonPlanTemplates")]
    public partial class CurriculumLessonPlanTemplate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public string PlanTemplate { get; set; }
    }
}
