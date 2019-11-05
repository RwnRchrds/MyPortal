using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class StaffHomeViewModel
    {
        public StaffMember CurrentUser { get; set; }
        public IEnumerable<CurriculumAcademicYear> CurriculumAcademicYears { get; set; }

        public int? SelectedAcademicYearId { get; set; }
    }
}