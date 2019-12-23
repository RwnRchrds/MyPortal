using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class LessonPlansViewModel
    {
        public LessonPlanDto LessonPlan { get; set; }
        public IEnumerable<StudyTopicDto> StudyTopics { get; set; }
    }
}