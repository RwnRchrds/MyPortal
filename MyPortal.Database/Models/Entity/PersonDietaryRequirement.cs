using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("PersonDietaryRequirements")]
    public class PersonDietaryRequirement : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid PersonId { get; set; }

        [Column(Order = 2)]
        public Guid DietaryRequirementId { get; set; }

        public virtual DietaryRequirement DietaryRequirement { get; set; }
        public virtual Person Person { get; set; }
    }
}