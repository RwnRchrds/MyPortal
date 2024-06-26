using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("LessonPlanTemplates")]
    public class LessonPlanTemplate : BaseTypes.Entity
    {
        [Column(Order = 2)]
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Column(Order = 3)] [Required] public string PlanTemplate { get; set; }
    }
}