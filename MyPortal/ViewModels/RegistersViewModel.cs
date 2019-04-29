using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class RegistersViewModel
    {
        public CoreStaffMember CurrentUser { get; set; }
        public IEnumerable<CoreStaffMember> StaffMembers { get; set; }
    }
}