using System.Collections.Generic;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class StudyTopicsViewModel
    {
        public StudyTopic StudyTopic { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public IEnumerable<YearGroup> YearGroups { get; set; }    
    }
}