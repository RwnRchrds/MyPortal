using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class StudentHomeViewModel
    {
        public Student Student { get; set; }
        public List<ProfileLog> Logs { get; set; }
        public IEnumerable<AssessmentResult> Results { get; set; }
        public bool IsUpperSchool { get; set; }
    }
}