using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("LessonPlans")]
    public class LessonPlan : BaseTypes.Entity, IDirectoryEntity, ICreatable

    {
        [Column(Order = 1)]
        public Guid StudyTopicId { get; set; }

        [Column(Order = 2)]
        public Guid CreatedById { get; set; }

        [Column(Order = 3)]
        public Guid DirectoryId { get; set; }

        [Column(Order = 4)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 5)]
        [Required] [StringLength(256)] public string Title { get; set; }

        [Column(Order = 6)]
        [Required] public string LearningObjectives { get; set; }

        [Column(Order = 7)]
        [Required] public string PlanContent { get; set; }

        [Column(Order = 8)]
        [Required] public string Homework { get; set; }

        public virtual Directory Directory { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual StudyTopic StudyTopic { get; set; }
    }
}
