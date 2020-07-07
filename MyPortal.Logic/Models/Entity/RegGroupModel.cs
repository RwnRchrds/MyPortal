using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class RegGroupModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public Guid TutorId { get; set; }

        public Guid YearGroupId { get; set; }

        public virtual StaffMemberModel Tutor { get; set; }

        public virtual YearGroupModel YearGroup { get; set; }
    }
}