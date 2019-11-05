using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class StudyTopicsViewModel
    {
        public CurriculumStudyTopic StudyTopic { get; set; }
        public IEnumerable<CurriculumSubject> Subjects { get; set; }
        public IEnumerable<PastoralYearGroup> YearGroups { get; set; }    
    }
}