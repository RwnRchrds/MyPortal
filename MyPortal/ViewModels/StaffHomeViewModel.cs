using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class StaffHomeViewModel
    {
        public StaffMember CurrentUser { get; set; }
        public IEnumerable<CurriculumAcademicYear> CurriculumAcademicYears { get; set; }
    }
}