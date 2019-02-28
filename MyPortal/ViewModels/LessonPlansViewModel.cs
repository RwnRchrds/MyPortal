using System.Collections.Generic;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class LessonPlansViewModel
    {
        public LessonPlan LessonPlan { get; set; }
        public IEnumerable<StudyTopic> StudyTopics { get; set; }    
    }
}