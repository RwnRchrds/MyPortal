using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    public class MedicalPersonDietaryRequirementDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int DietaryRequirementId { get; set; }

        public virtual MedicalDietaryRequirementDto DietaryRequirement { get; set; }
        public virtual PersonDto Person { get; set; }
    }
}