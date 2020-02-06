using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("SubjectStaffMember")]
    public class SubjectStaffMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid SubjectId { get; set; }

        public Guid StaffMemberId { get; set; }

        public Guid RoleId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual StaffMember StaffMember { get; set; }
        public virtual SubjectStaffMemberRole Role { get; set; }
    }
}