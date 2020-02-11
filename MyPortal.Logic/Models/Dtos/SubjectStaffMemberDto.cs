using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class SubjectStaffMemberDto
    {
        public Guid Id { get; set; }

        public Guid SubjectId { get; set; }

        public Guid StaffMemberId { get; set; }

        public Guid RoleId { get; set; }

        public virtual SubjectDto Subject { get; set; }
        public virtual StaffMemberDto StaffMember { get; set; }
        public virtual SubjectStaffMemberRoleDto Role { get; set; }
    }
}
