using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class ReportDto
    {
        public int Id { get; set; }

        public int AreaId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        public bool Restricted { get; set; }

        public virtual SystemAreaDto SystemArea { get; set; }
    }
}
