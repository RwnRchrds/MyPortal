using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("PersonDietaryRequirements")]
    public class PersonDietaryRequirement : Entity
    {
        [Column(Order = 1)]
        public Guid PersonId { get; set; }

        [Column(Order = 2)]
        public Guid DietaryRequirementId { get; set; }

        public virtual DietaryRequirement DietaryRequirement { get; set; }
        public virtual Person Person { get; set; }
    }
}