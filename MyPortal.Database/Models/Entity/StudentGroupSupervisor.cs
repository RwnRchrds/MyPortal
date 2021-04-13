using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("StudentGroupSupervisors")]
    public class StudentGroupSupervisor : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid StudentGroupId { get; set; }
        
        [Column(Order = 2)]
        public Guid SupervisorTitleId { get; set; }
        
        [Column(Order = 3)]
        public Guid SupervisorId { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }
        public virtual StudentGroupSupervisorTitle SupervisorTitle { get; set; }
        public virtual StaffMember Supervisor { get; set; }
    }
}