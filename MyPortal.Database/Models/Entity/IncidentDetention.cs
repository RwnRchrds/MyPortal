using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("IncidentDetentions")]
    public class IncidentDetention : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid IncidentId { get; set; }

        [Column(Order = 2)]
        public Guid DetentionId { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Detention Detention { get; set; }
    }
}
