using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class PhoneNumberDto
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public Guid PersonId { get; set; }

        [Phone]
        [StringLength(128)]
        public string Number { get; set; }

        public virtual PhoneNumberTypeDto Type { get; set; }
        public virtual PersonDto Person { get; set; }
    }
}
