using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("LessonPlan")]
    public partial class LessonPlan
    {
        public int Id { get; set; }

        public int StudyTopicId { get; set; }

        public int AuthorId { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        public string LearningObjectives { get; set; }

        [Required]
        public string PlanContent { get; set; }

        [Required]
        public string Homework { get; set; }

        public virtual StaffMember Author { get; set; }

        public virtual StudyTopic StudyTopic { get; set; }
    }
}
