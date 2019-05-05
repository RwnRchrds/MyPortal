using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class RandomStudentViewModel
    {
        public PeopleStudent Student { get; set; }
        public List<AssessmentResult> Results { get; set; }
    }
}