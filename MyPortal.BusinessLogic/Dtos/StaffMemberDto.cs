using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Interfaces;
using MyPortal.Data.Interfaces;

namespace MyPortal.BusinessLogic.Dtos
{
    public class StaffMemberDto : IPersonDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        [Required]
        [Index(IsUnique = true)]
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
