using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// [NOT CURRENTLY IN USE] A generic template for lesson plan creation.
    /// </summary>
    [Table("LessonPlanTemplate")]
    public partial class LessonPlanTemplate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public string PlanTemplate { get; set; }
    }
}
