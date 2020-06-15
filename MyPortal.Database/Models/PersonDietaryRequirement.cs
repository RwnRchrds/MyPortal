using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("PersonDietaryRequirement")]
    public class PersonDietaryRequirement
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid PersonId { get; set; }

        [DataMember]
        public Guid DietaryRequirementId { get; set; }

        public virtual DietaryRequirement DietaryRequirement { get; set; }
        public virtual Person Person { get; set; }
    }
}