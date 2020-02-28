using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Details
{
    public class HouseDetails
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public Guid? HeadId { get; set; }

        [StringLength(128)]
        public string ColourCode { get; set; }

        public virtual StaffMemberDetails HeadOfHouse { get; set; }
    }
}