using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("PersonCondition")]
    public class PersonCondition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Guid ConditionId { get; set; }

        public bool MedicationTaken { get; set; }

        [StringLength(256)]
        public string Medication { get; set; }

        public virtual Person Person { get; set; }
        public virtual MedicalCondition MedicalCondition { get; set; }
    }
}