using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class StaffMemberDto
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

        public virtual PersonDto Person { get; set; }

        public string GetDisplayName()
        {
            return $"{Person.Title} {Person.FirstName.Substring(0, 1)} {Person.LastName}";
        }
    }
}
