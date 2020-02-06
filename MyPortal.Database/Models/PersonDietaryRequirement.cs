using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("PersonDietaryRequirement")]
    public class PersonDietaryRequirement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Guid DietaryRequirementId { get; set; }

        public virtual DietaryRequirement DietaryRequirement { get; set; }
        public virtual Person Person { get; set; }
    }
}