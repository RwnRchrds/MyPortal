using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    [Table("PersonDietaryRequirement", Schema = "document")]
    public class PersonDietaryRequirement
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int DietaryRequirementId { get; set; }

        public virtual DietaryRequirement DietaryRequirement { get; set; }
        public virtual Person Person { get; set; }
    }
}