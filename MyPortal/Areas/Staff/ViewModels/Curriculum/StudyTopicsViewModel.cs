using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels.Curriculum
{
    public class StudyTopicsViewModel
    {
        public StudyTopicDto StudyTopic { get; set; }
        public IEnumerable<SubjectDto> Subjects { get; set; }
        public IEnumerable<YearGroupDto> YearGroups { get; set; }
    }
}