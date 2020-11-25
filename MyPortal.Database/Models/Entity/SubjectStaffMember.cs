using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("SubjectStaffMembers")]
    public class SubjectStaffMember : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid SubjectId { get; set; }

        [Column(Order = 2)]
        public Guid StaffMemberId { get; set; }

        [Column(Order = 3)]
        public Guid RoleId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual StaffMember StaffMember { get; set; }
        public virtual SubjectStaffMemberRole Role { get; set; }
    }
}