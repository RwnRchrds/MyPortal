using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class AgentModel : BaseModel
    {
        public Guid AgencyId { get; set; }

        public Guid PersonId { get; set; }

        [StringLength(128)]
        public string JobTitle { get; set; }

        public bool Deleted { get; set; }

        public virtual AgencyModel Agency { get; set; }
        public virtual PersonModel Person { get; set; }
    }
}
