using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("Detentions")]
    public class Detention : BaseTypes.Entity
    {
        public Detention()
        {
            Students = new HashSet<StudentDetention>();
        }

        [Column(Order = 2)]
        public Guid DetentionTypeId { get; set; }

        [Column(Order = 3)]
        public Guid EventId { get; set; }

        [Column(Order = 4)]
        public Guid? SupervisorId { get; set; }

        public virtual DetentionType Type { get; set; }
        public virtual DiaryEvent Event { get; set; }
        public virtual StaffMember Supervisor { get; set; }
        public virtual ICollection<StudentDetention> Students { get; set; }
    }
}
