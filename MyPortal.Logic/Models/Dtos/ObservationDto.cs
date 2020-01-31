using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class ObservationDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ObserveeId { get; set; }

        public int ObserverId { get; set; }

        public int OutcomeId { get; set; }

        public virtual StaffMemberDto Observee { get; set; }

        public virtual StaffMemberDto Observer { get; set; }

        public virtual ObservationOutcomeDto Outcome { get; set; }
    }
}
