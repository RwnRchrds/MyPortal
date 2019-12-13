using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    [Table("PersonCondition", Schema = "medical")]
    public class PersonCondition
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int ConditionId { get; set; }

        public bool MedicationTaken { get; set; }

        [StringLength(256)]
        public string Medication { get; set; }

        public virtual Person Person { get; set; }
        public virtual Condition Condition { get; set; }
    }
}