using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("Detention")]
    public class Detention
    {
        public Detention()
        {
            Incidents = new HashSet<IncidentDetention>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid DetentionTypeId { get; set; }
        public Guid EventId { get; set; }
        public Guid? SupervisorId { get; set; }

        public virtual DetentionType Type { get; set; }
        public virtual DiaryEvent Event { get; set; }
        public virtual StaffMember Supervisor { get; set; }
        public virtual ICollection<IncidentDetention> Incidents { get; set; }
    }
}
