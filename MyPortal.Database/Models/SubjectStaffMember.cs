using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("SubjectStaffMember")]
    public class SubjectStaffMember
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid SubjectId { get; set; }

        [DataMember]
        public Guid StaffMemberId { get; set; }

        [DataMember]
        public Guid RoleId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual StaffMember StaffMember { get; set; }
        public virtual SubjectStaffMemberRole Role { get; set; }
    }
}