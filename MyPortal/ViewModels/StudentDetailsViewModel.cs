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
    }
}