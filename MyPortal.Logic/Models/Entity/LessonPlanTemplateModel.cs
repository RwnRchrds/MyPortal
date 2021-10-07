using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class LessonPlanTemplateModel : BaseModel
    {
        public LessonPlanTemplateModel(LessonPlanTemplate model) : base(model)
        {
            Name = model.Name;
            PlanTemplate = model.PlanTemplate;
        }
        
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public string PlanTemplate { get; set; }
    }
}
