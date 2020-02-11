using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class AddressPersonDto
    {
        public Guid Id { get; set; }

        public Guid AddressId { get; set; }

        public Guid PersonId { get; set; }

        public virtual AddressDto Address { get; set; }
        public virtual PersonDto Person { get; set; }
    }
}
