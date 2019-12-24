﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    [Table("SubjectStaffMember", Schema = "curriculum")]
    public class SubjectStaffMember
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int StaffMemberId { get; set; }

        public int RoleId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual StaffMember StaffMember { get; set; }
        public virtual SubjectStaffMemberRole Role { get; set; }
    }
}