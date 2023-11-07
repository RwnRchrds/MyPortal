using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Observations")]
    public class Observation : BaseTypes.Entity
    {
        [Column(Order = 2, TypeName = "date")] public DateTime Date { get; set; }

        [Column(Order = 3)] public Guid ObserveeId { get; set; }

        [Column(Order = 4)] public Guid ObserverId { get; set; }

        [Column(Order = 5)] public Guid OutcomeId { get; set; }

        [Column(Order = 6)] public string Notes { get; set; }

        public virtual StaffMember Observee { get; set; }

        public virtual StaffMember Observer { get; set; }

        public virtual ObservationOutcome Outcome { get; set; }
    }
}