using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("LessonPlan")]
    public partial class LessonPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid StudyTopicId { get; set; }

        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        public string LearningObjectives { get; set; }

        [Required]
        public string PlanContent { get; set; }

        [Required]
        public string Homework { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual StudyTopic StudyTopic { get; set; }
    }
}
