using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Details
{
    public class YearGroupDetails
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public Guid? HeadId { get; set; }

        public int KeyStage { get; set; }

        public virtual StaffMemberDetails HeadOfYear { get; set; }
    }
}