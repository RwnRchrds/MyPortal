using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class RegistersViewModel
    {
        public StaffMemberDto CurrentUser { get; set; }
        public IEnumerable<StaffMemberDto> StaffMembers { get; set; }
    }
}