using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Business
{
    public class HouseModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public Guid? HeadId { get; set; }

        [StringLength(128)]
        public string ColourCode { get; set; }

        public virtual StaffMemberModel HeadOfHouse { get; set; }
    }
}