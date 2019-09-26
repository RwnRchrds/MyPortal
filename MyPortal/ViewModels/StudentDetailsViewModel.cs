using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class StudentDetailsViewModel
    {
        public StudentDetailsViewModel()
        {
            Genders = Gender.GetGenderOptions();
        }
        
        public Student Student { get; set; }
        public IEnumerable<ProfileLogType> LogTypes { get; set; }
        public ProfileLog Log { get; set; }
        public IEnumerable<PastoralYearGroup> YearGroups { get; set; }
        public IEnumerable<PastoralRegGroup> RegGroups { get; set; }
        public IEnumerable<AssessmentResultSet> ResultSets { get; set; }
        public IEnumerable<CurriculumSubject> Subjects { get; set; }
        public IEnumerable<Gender> Genders { get; set; }
        public IEnumerable<ProfileCommentBank> CommentBanks { get; set; }
        public PersonDocumentUpload Upload { get; set; }
        public bool HasAttendaceData { get; set; }
        public double? Attendance { get; set; }
        public int? AchievementCount { get; set; }
        public int? BehaviourCount { get; set; }
    }
}