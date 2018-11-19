using System.Collections.Generic;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class StudentHomeViewModel
    {
        public Student Student { get; set; }
        public List<Log> Logs { get; set; }
        public IEnumerable<Result> Results { get; set; }
        public bool IsUpperSchool { get; set; }
    }
}