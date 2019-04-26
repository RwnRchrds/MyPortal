using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class StudentResultsViewModel
    {
        public CoreStudent Student { get; set; }
        public IEnumerable<AssessmentResultSet> ResultSets { get; set; }
        public int CurrentResultSetId { get; set; }
        public AssessmentResult Result { get; set; }
        public IEnumerable<CurriculumSubject> Subjects { get; set; }
        public IEnumerable<AssessmentGradeSet> GradeSets { get; set; }
    }
}