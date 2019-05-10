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
        //public List<Log> Logs { get; set; }
        public IEnumerable<AssessmentResult> Results { get; set; }
        //public bool IsUpperSchool { get; set; }
        public IEnumerable<ProfileLogType> LogTypes { get; set; }
        public ProfileLog Log { get; set; }
        public IEnumerable<PastoralYearGroup> YearGroups { get; set; }
        public IEnumerable<PastoralRegGroup> RegGroups { get; set; }
        
        public AssessmentResult Result { get; set; }
        public IEnumerable<AssessmentResultSet> ResultSets { get; set; }
        public IEnumerable<CurriculumSubject> Subjects { get; set; }
        public IEnumerable<Gender> Genders { get; set; }
        public IEnumerable<ProfileCommentBank> CommentBanks { get; set; }
        public StudentDocumentUpload Upload { get; set; }
    }
}