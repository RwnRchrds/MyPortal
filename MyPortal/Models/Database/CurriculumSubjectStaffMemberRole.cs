using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("Curriculum_SubjectStaffMemberRoles")]
    public class CurriculumSubjectStaffMemberRole
    {
        public CurriculumSubjectStaffMemberRole()
        {
            StaffMembers = new HashSet<CurriculumSubjectStaffMember>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CurriculumSubjectStaffMember> StaffMembers { get; set; }
    }
}