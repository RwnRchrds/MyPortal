using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class LessonPlanDto
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

        public virtual StaffMemberDto Author { get; set; }

        public virtual StudyTopicDto StudyTopic { get; set; }
    }
}
