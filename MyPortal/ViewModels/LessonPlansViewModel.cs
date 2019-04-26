using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class LessonPlansViewModel
    {
        public CurriculumLessonPlan LessonPlan { get; set; }
        public IEnumerable<CurriculumStudyTopic> StudyTopics { get; set; }    
    }
}