using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// An appraisal/observation carried out by line managers on members of staff.
    /// </summary>
    [Table("Observation")]
    public partial class Observation
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int ObserveeId { get; set; }

        public int ObserverId { get; set; }

        public int OutcomeId { get; set; }

        public virtual StaffMember Observee { get; set; }

        public virtual StaffMember Observer { get; set; }

        public virtual ObservationOutcome Outcome { get; set; }
    }
}
