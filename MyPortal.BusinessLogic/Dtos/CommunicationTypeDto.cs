using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class CommunicationTypeDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Description { get; set; }
    }
}
