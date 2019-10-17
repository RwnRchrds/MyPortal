namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A lesson plan for a study topic.
    /// </summary>
    [Table("Curriculum_LessonPlans")]
    public partial class CurriculumLessonPlan
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

        public virtual CurriculumStudyTopic StudyTopic { get; set; }
    }
}
