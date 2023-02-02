using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Curriculum
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
