using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class PhoneNumberModel : BaseModel
    {
        public Guid TypeId { get; set; }

        public Guid? PersonId { get; set; }

        public Guid? AgencyId { get; set; }

        [Phone]
        [StringLength(128)]
        public string Number { get; set; }

        public bool Main { get; set; }

        public virtual PhoneNumberTypeModel Type { get; set; }
        public virtual PersonModel Person { get; set; }
    }
}
