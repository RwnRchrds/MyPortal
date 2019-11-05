using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Students.ViewModels
{
    public class StudentResultsViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<AssessmentResultSet> ResultSets { get; set; }
        public AssessmentResult Result { get; set; }
        public IEnumerable<CurriculumSubject> Subjects { get; set; }
    }
}