using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Models.Dtos
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

        public virtual StaffMember HeadOfHouse { get; set; }
    }
}
