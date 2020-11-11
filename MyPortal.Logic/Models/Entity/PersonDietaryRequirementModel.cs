using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class PersonDietaryRequirementModel : BaseModel
    {
        public Guid PersonId { get; set; }

        public Guid DietaryRequirementId { get; set; }

        public virtual DietaryRequirementModel DietaryRequirement { get; set; }
        public virtual PersonModel Person { get; set; }
    }
}
