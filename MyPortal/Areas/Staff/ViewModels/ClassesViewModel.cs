using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class ClassesViewModel
    {
        public ClassDto Class { get; set; }
        public IEnumerable<StaffMemberDto> Staff { get; set; }
        public IEnumerable<SubjectDto> Subjects { get; set; }
    }
}