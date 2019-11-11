using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("Curriculum_SubjectStaff")]
    public class CurriculumSubjectStaffMember
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int StaffMemberId { get; set; }

        [Required]
        [StringLength(256)]
        public string Role { get; set; }

        public virtual CurriculumSubject Subject { get; set; }
        public virtual StaffMember StaffMember { get; set; }
    }
}