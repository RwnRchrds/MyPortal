using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Models.Dtos
{
    public class RegGroupDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public Guid TutorId { get; set; }

        public Guid YearGroupId { get; set; }

        public virtual StaffMemberDto Tutor { get; set; }

        public virtual YearGroupDto YearGroup { get; set; }
    }
}
