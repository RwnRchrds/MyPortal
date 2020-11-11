using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class SubjectStaffMemberModel : BaseModel
    {
        public Guid SubjectId { get; set; }
        
        public Guid StaffMemberId { get; set; }
        
        public Guid RoleId { get; set; }

        public virtual SubjectModel Subject { get; set; }
        public virtual StaffMemberModel StaffMember { get; set; }
        public virtual SubjectStaffMemberRoleModel Role { get; set; }
    }
}