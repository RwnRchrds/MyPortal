using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class ObservationDto
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid ObserveeId { get; set; }

        public Guid ObserverId { get; set; }

        public Guid OutcomeId { get; set; }

        public virtual StaffMemberDto Observee { get; set; }

        public virtual StaffMemberDto Observer { get; set; }

        public virtual ObservationOutcomeDto Outcome { get; set; }
    }
}
