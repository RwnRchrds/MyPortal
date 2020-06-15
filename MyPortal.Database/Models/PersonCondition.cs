using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("PersonCondition")]
    public class PersonCondition
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid PersonId { get; set; }

        [DataMember]
        public Guid ConditionId { get; set; }

        [DataMember]
        public bool MedicationTaken { get; set; }

        [DataMember]
        [StringLength(256)]
        public string Medication { get; set; }

        public virtual Person Person { get; set; }
        public virtual MedicalCondition MedicalCondition { get; set; }
    }
}