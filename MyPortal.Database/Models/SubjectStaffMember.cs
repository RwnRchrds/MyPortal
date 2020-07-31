using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("SubjectStaffMembers")]
    public class SubjectStaffMember : Entity
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