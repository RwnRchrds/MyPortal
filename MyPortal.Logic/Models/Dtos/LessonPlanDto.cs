using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class LessonPlanDto
    {
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

        public virtual StaffMemberDto Author { get; set; }

        public virtual StudyTopicDto StudyTopic { get; set; }
    }
}
