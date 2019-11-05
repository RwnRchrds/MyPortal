using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class RegistersViewModel
    {
        public StaffMember CurrentUser { get; set; }
        public IEnumerable<StaffMember> StaffMembers { get; set; }
    }
}