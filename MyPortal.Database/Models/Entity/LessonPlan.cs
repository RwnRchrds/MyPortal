using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("LessonPlans")]
    public partial class LessonPlan : BaseTypes.Entity

    {
        [Column(Order = 1)]
        public Guid StudyTopicId { get; set; }

        [Column(Order = 2)]
        public Guid AuthorId { get; set; }

        [Column(Order = 3)]
        public Guid DirectoryId { get; set; }

        [Column(Order = 4)]
        [Required] [StringLength(256)] public string Title { get; set; }

        [Column(Order = 5)]
        [Required] public string LearningObjectives { get; set; }

        [Column(Order = 6)]
        [Required] public string PlanContent { get; set; }

        [Column(Order = 7)]
        [Required] public string Homework { get; set; }

        public virtual Directory Directory { get; set; }
        public virtual User Author { get; set; }
        public virtual StudyTopic StudyTopic { get; set; }
    }
}
