using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class LocalAuthorityDto
    {
        public Guid Id { get; set; }

        public int LeaCode { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public string Website { get; set; }
    }
}
