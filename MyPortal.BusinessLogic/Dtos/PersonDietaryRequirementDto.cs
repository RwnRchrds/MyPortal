using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class PersonDietaryRequirementDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int DietaryRequirementId { get; set; }

        public virtual DietaryRequirementDto DietaryRequirement { get; set; }
        public virtual PersonDto Person { get; set; }
    }
}
