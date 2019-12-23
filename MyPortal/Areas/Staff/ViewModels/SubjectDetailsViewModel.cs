using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class SubjectDetailsViewModel
    {
        public SubjectDto Subject { get; set; }
        public IDictionary<int, string> StaffMembers { get; set; }
        public IDictionary<int, string> SubjectRoles { get; set; }
        public SubjectStaffMemberDto SubjectStaffMember { get; set; }
    }
}