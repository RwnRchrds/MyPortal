using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Dtos
{
    public class HouseDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public int? HeadId { get; set; }

        [StringLength(128)]
        public string ColourCode { get; set; }

        public virtual StaffMemberDto HeadOfHouse { get; set; }
    }
}
