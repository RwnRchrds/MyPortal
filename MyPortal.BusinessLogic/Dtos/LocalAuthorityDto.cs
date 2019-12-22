using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class LocalAuthorityDto
    {
        public int Id { get; set; }

        public int LeaCode { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public string Website { get; set; }
    }
}
