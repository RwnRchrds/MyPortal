using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    public class CurriculumSubjectStaffMemberDto
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int StaffMemberId { get; set; }
        public int RoleId { get; set; }

        public virtual CurriculumSubjectDto Subject { get; set; }
        public virtual StaffMemberDto StaffMember { get; set; }
        public virtual CurriculumSubjectStaffMemberRoleDto Role { get; set; }
    }
}