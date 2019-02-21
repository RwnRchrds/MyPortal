using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    [Table("Curriculum_LessonPlans")]
    public class LessonPlan
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Study Topic")]
        public int StudyTopicId { get; set; }
        
        [Required] public string Title { get; set; }

        [Required]
        public string PlanContent { get; set; }

        public virtual StudyTopic StudyTopic { get; set; }
    }
}