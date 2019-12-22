using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class PhoneNumberDto
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int PersonId { get; set; }

        [Phone]
        [StringLength(128)]
        public string Number { get; set; }

        public virtual PhoneNumberTypeDto Type { get; set; }
        public virtual PersonDto Person { get; set; }
    }
}
