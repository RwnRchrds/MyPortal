using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("PersonConditions")]
    public class PersonCondition : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid PersonId { get; set; }

        [Column(Order = 2)]
        public Guid ConditionId { get; set; }

        [Column(Order = 3)]
        public bool MedicationTaken { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string Medication { get; set; }

        public virtual Person Person { get; set; }
        public virtual MedicalCondition MedicalCondition { get; set; }
    }
}