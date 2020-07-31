using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("SubjectStaffMemberRoles")]
    public class SubjectStaffMemberRole : LookupItem
    {
        public SubjectStaffMemberRole()
        {
            StaffMembers = new HashSet<SubjectStaffMember>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectStaffMember> StaffMembers { get; set; }
    }
}