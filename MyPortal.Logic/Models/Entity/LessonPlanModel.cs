using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class LessonPlanModel
    {
        public Guid Id { get; set; }

        public Guid StudyTopicId { get; set; }

        public Guid AuthorId { get; set; }

        public Guid DirectoryId { get; set; }

        [Required] 
        [StringLength(256)] public string Title { get; set; }

        [Required] public string LearningObjectives { get; set; }

        [Required] public string PlanContent { get; set; }

        [Required] public string Homework { get; set; }

        public virtual DirectoryModel Directory { get; set; }
        public virtual UserModel Author { get; set; }
        public virtual StudyTopicModel StudyTopic { get; set; }
    }
}
