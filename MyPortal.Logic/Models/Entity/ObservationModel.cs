using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ObservationModel : BaseModel
    {
        public DateTime Date { get; set; }
        public Guid ObserveeId { get; set; }
        public Guid ObserverId { get; set; }
        public Guid OutcomeId { get; set; }

        public virtual StaffMemberModel Observee { get; set; }
        public virtual StaffMemberModel Observer { get; set; }
        public virtual ObservationOutcomeModel Outcome { get; set; }
    }
}
