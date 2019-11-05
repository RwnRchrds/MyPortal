using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class SubjectsViewModel
    {
        public CurriculumSubject Subject { get; set; }
        public IEnumerable<StaffMember> Staff { get; set; }
    }
}