using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("Medical_PersonDietaryRequirements")]
    public class MedicalPersonDietaryRequirement
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int DietaryRequirementId { get; set; }

        public virtual MedicalDietaryRequirement DietaryRequirement { get; set; }
        public virtual Person Person { get; set; }
    }
}