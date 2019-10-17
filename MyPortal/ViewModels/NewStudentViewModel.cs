using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class NewStudentViewModel 
    {
        public IEnumerable<PastoralYearGroup> YearGroups { get; set; }
        public IEnumerable<PastoralRegGroup> RegGroups { get; set; }
        public Student Student { get; set; }
    }
}