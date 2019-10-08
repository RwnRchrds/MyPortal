using MyPortal.Models.Database;

namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// An appraisal/observation carried out by line managers on members of staff.
    /// </summary>
    
    public partial class PersonnelObservationDto
    {
        public int Id { get; set; }

        
        public DateTime Date { get; set; }

        public int ObserveeId { get; set; }

        public int ObserverId { get; set; }

        
        public ObservationOutcome Outcome { get; set; }

        public virtual StaffMemberDto Observee { get; set; }

        public virtual StaffMemberDto Observer { get; set; }
    }
}
