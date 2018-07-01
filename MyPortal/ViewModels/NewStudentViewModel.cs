using System.Collections.Generic;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class NewStudentViewModel
    {
        public IEnumerable<YearGroup> YearGroups { get; set; }
        public IEnumerable<RegGroup> RegGroups { get; set; }
        public Student Student { get; set; }
    }
}