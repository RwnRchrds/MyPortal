using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Business
{
    public class StaffMemberModel
    {
        public Guid Id { get; set; }

        public Guid? LineManagerId { get; set; }

        public Guid PersonId { get; set; }

        [Required]
        [StringLength(128)]
        public string Code { get; set; }

        [StringLength(128)]
        public string NiNumber { get; set; }

        [StringLength(128)]
        public string PostNominal { get; set; }

        public bool TeachingStaff { get; set; }

        public bool Deleted { get; set; }

        public virtual PersonModel Person { get; set; }

        public virtual StaffMemberModel LineManager { get; set; }
    }
}