using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Details
{
    public class StaffMemberDetails
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        [StringLength(128)]
        public string NiNumber { get; set; }

        [StringLength(128)]
        public string PostNominal { get; set; }

        [DefaultValue(false)]
        public bool TeachingStaff { get; set; }

        public bool Deleted { get; set; }

        public virtual PersonDetails Person { get; set; }
    }
}