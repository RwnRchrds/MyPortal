using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class AddressPersonModel : BaseModel
    {
        public Guid AddressId { get; set; }

        public Guid PersonId { get; set; }

        public virtual AddressModel Address { get; set; }
        public virtual PersonModel Person { get; set; }
    }
}
