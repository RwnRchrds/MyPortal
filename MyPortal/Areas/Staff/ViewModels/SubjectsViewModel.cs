using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class SubjectsViewModel
    {
        public SubjectDto Subject { get; set; }
        public IEnumerable<StaffMemberDto> Staff { get; set; }
    }
}