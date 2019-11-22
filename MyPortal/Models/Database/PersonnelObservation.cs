namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public enum ObservationOutcome
    {
        Outstanding = 1,

        Good = 2,

        Satisfactory = 3,

        Inadequate = 4
    }

    /// <summary>
    /// An appraisal/observation carried out by line managers on members of staff.
    /// </summary>
    [Table("Personnel_Observations")]
    public partial class PersonnelObservation
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int ObserveeId { get; set; }

        public int ObserverId { get; set; }

        public ObservationOutcome Outcome { get; set; }

        public virtual StaffMember Observee { get; set; }

        public virtual StaffMember Observer { get; set; }
    }
}
