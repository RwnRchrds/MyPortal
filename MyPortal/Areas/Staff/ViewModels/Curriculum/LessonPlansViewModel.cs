using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels.Curriculum
{
    public class LessonPlansViewModel
    {
        public LessonPlanDto LessonPlan { get; set; }
        public IEnumerable<StudyTopicDto> StudyTopics { get; set; }
    }
}