using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("PersonConditions")]
    public class PersonCondition : Entity
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