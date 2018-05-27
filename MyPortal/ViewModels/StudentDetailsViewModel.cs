using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class StudentDetailsViewModel
    {
        public Student Student { get; set; }
        public List<Log> Logs { get; set; }
        public IEnumerable<Result> Results { get; set; }
        public bool IsUpperSchool { get; set; }
        public ChartData ChartData { get; set; }
        public IEnumerable<LogType> LogTypes { get; set; }
        public Log Log { get; set; }
        public IEnumerable<YearGroup> YearGroups { get; set; }
        public IEnumerable<RegGroup> RegGroups { get; set; }
        public Result Result { get; set; }
        public IEnumerable<ResultSet> ResultSets { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
    }
}