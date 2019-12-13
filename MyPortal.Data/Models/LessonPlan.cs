using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// A lesson plan for a study topic.
    /// </summary>
    [Table("LessonPlan", Schema = "curriculum")]
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
