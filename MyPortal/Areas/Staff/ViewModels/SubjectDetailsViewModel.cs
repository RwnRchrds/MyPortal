using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class SubjectDetailsViewModel
    {
        public CurriculumSubject Subject { get; set; }
        public IDictionary<int, string> StaffMembers { get; set; }
        public IDictionary<int, string> SubjectRoles { get; set; }
        public CurriculumSubjectStaffMember SubjectStaffMember { get; set; }
    }
}