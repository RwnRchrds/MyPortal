using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class EmailAddressDto
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public int PersonId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(128)]
        public string Address { get; set; }

        public bool Main { get; set; }
        public bool Primary { get; set; }
        public string Notes { get; set; }

        public virtual PersonDto Person { get; set; }
        public virtual EmailAddressTypeDto Type { get; set; }
    }
}
