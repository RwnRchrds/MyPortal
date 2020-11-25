using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ActivitySupervisors")]
    public class ActivitySupervisor : Entity
    {
        [Column(Order = 1)]
        public Guid ActivityId { get; set; }

        [Column(Order = 2)]
        public Guid SupervisorId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual StaffMember Supervisor { get; set; }
    }
}
