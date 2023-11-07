using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("StudentGroupSupervisors")]
    public class StudentGroupSupervisor : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid StudentGroupId { get; set; }

        [Column(Order = 3)] public Guid SupervisorId { get; set; }

        [Column(Order = 4)] public string Title { get; set; }

        public virtual StudentGroup StudentGroup { get; set; }
        public virtual StaffMember Supervisor { get; set; }
        public virtual ICollection<StudentGroup> MainGroups { get; set; }
    }
}