using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.VisualBasic.CompilerServices;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    [Table("LessonPlans")]
    public partial class LessonPlan : Entity

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
        public virtual ApplicationUser Author { get; set; }
        public virtual StudyTopic StudyTopic { get; set; }
    }
}
