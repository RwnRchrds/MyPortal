using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class SubjectStaffMemberDto
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int StaffMemberId { get; set; }

        public int RoleId { get; set; }

        public virtual SubjectDto Subject { get; set; }
        public virtual StaffMemberDto StaffMember { get; set; }
        public virtual SubjectStaffMemberRoleDto Role { get; set; }
    }
}
