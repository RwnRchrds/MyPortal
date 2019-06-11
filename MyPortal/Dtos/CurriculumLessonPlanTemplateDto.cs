namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// [NOT IN USE] A generic template for lesson plan creation.
    /// </summary>
    public partial class CurriculumLessonPlanTemplateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PlanTemplate { get; set; }
    }
}
