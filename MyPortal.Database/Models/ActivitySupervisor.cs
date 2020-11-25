using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    public class ActivitySupervisor : Entity
    {
        public Guid ActivityId { get; set; }
        public Guid SupervisorId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual StaffMember Supervisor { get; set; }
    }
}
