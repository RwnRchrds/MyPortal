using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Models.Data;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class StudentOverviewViewModel
    {
        public StudentDto Student { get; set; }
        public IDictionary<int, string> LogTypes { get; set; }
        public ProfileLogNoteDto LogNote { get; set; }
        public PersonDocumentUpload Upload { get; set; }
        public IDictionary<int, string> CommentBanks { get; set; }
        public bool HasAttendaceData { get; set; }
        public double? Attendance { get; set; }
        public int? AchievementCount { get; set; }
        public int? BehaviourCount { get; set; }
    }
}