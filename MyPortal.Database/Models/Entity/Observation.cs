using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Observations")]
    public class Observation : BaseTypes.Entity
    {
        [Column(Order = 1, TypeName = "date")]
        public DateTime Date { get; set; }

        [Column(Order = 2)]
        public Guid ObserveeId { get; set; }

        [Column(Order = 3)]
        public Guid ObserverId { get; set; }

        [Column(Order = 4)]
        public Guid OutcomeId { get; set; }

        public virtual StaffMember Observee { get; set; }

        public virtual StaffMember Observer { get; set; }

        public virtual ObservationOutcome Outcome { get; set; }
    }
}
