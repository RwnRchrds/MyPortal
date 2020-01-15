using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels.Staff
{
    public class StaffHomeViewModel
    {
        public StaffMemberDto CurrentUser { get; set; }
        public IEnumerable<AcademicYearDto> CurriculumAcademicYears { get; set; }

        public int? SelectedAcademicYearId { get; set; }
    }
}