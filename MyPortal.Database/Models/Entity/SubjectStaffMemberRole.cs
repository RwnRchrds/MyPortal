using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("SubjectStaffMemberRoles")]
    public class SubjectStaffMemberRole : LookupItem, ISystemEntity
    {
        public SubjectStaffMemberRole()
        {
            StaffMembers = new HashSet<SubjectStaffMember>();
        }

        public bool System { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubjectStaffMember> StaffMembers { get; set; }
    }
}