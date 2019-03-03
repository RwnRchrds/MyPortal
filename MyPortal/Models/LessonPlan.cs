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
        [Display(Name = "Learning Objectives")]
            public string LearningObjectives { get; set; }

        [Required]
        [Display(Name = "Lesson Content")]
        public string PlanContent { get; set; }
        
        [Required]
        public string Homework { get; set; }
                 
        public int AuthorId { get; set; }

        public virtual StudyTopic StudyTopic { get; set; }
        public virtual Staff Author { get; set; }
    }
}