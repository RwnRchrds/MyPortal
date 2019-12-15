using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Dtos
{
    public class AddressPersonDto
    {
        public int Id { get; set; }

        public int AddressId { get; set; }

        public int PersonId { get; set; }

        public virtual AddressDto Address { get; set; }
        public virtual PersonDto Person { get; set; }
    }
}
