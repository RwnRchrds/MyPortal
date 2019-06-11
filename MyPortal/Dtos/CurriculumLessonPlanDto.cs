namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A lesson plan for a study topic.
    /// </summary>
    public partial class CurriculumLessonPlanDto
    {
        public int Id { get; set; }

        public int StudyTopicId { get; set; }

        public string Title { get; set; }

        public string LearningObjectives { get; set; }

        public string PlanContent { get; set; }

        public string Homework { get; set; }

        public int AuthorId { get; set; }

        public virtual StaffMemberDto Author { get; set; }

        public virtual CurriculumStudyTopicDto StudyTopic { get; set; }
    }
}
