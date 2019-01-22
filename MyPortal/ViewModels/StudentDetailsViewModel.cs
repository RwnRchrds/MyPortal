using System.Collections.Generic;
using MyPortal.Models;
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
        public IEnumerable<Result> Results { get; set; }
        //public bool IsUpperSchool { get; set; }
        public IEnumerable<LogType> LogTypes { get; set; }
        public Log Log { get; set; }
        public IEnumerable<YearGroup> YearGroups { get; set; }
        public IEnumerable<RegGroup> RegGroups { get; set; }
        
        public Result Result { get; set; }
        public IEnumerable<ResultSet> ResultSets { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public IEnumerable<Gender> Genders { get; set; }
        public StudentDocumentUpload Upload { get; set; }
    }
}