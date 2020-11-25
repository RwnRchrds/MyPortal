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
            Incidents = new HashSet<IncidentDetention>();
        }

        [Column(Order = 1)]
        public Guid DetentionTypeId { get; set; }

        [Column(Order = 2)]
        public Guid EventId { get; set; }

        [Column(Order = 3)]
        public Guid? SupervisorId { get; set; }

        public virtual DetentionType Type { get; set; }
        public virtual DiaryEvent Event { get; set; }
        public virtual StaffMember Supervisor { get; set; }
        public virtual ICollection<IncidentDetention> Incidents { get; set; }
    }
}
