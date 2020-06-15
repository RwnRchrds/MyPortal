using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Observation")]
    public class Observation
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [DataMember]
        public Guid ObserveeId { get; set; }

        [DataMember]
        public Guid ObserverId { get; set; }

        [DataMember]
        public Guid OutcomeId { get; set; }

        public virtual StaffMember Observee { get; set; }

        public virtual StaffMember Observer { get; set; }

        public virtual ObservationOutcome Outcome { get; set; }
    }
}
