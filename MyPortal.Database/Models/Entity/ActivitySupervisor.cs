using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ActivitySupervisors")]
    public class ActivitySupervisor : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ActivityId { get; set; }

        [Column(Order = 2)]
        public Guid SupervisorId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual StaffMember Supervisor { get; set; }
    }
}
