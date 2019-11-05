using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class ClassesViewModel
    {
        public CurriculumClass Class { get; set; }
        public IEnumerable<StaffMember> Staff { get; set; }
        public IEnumerable<CurriculumSubject> Subjects { get; set; }   
    }
}