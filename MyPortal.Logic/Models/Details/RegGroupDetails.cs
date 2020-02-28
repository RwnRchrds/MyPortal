using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Details
{
    public class RegGroupDetails
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public Guid TutorId { get; set; }

        public Guid YearGroupId { get; set; }

        public virtual StaffMemberDetails Tutor { get; set; }

        public virtual YearGroupDetails YearGroup { get; set; }
    }
}