using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("StudentIncidentDetentions")]
    public class StudentIncidentDetention : BaseTypes.Entity
    {
        [Column(Order = 2)]
        public Guid StudentIncidentId { get; set; }

        [Column(Order = 3)]
        public Guid DetentionId { get; set; }

        [Column(Order = 4)]
        public bool Attended { get; set; }

        [Column(Order = 5)]
        public string Notes { get; set; }

        public virtual StudentIncident StudentIncident { get; set; }
        public virtual Detention Detention { get; set; }
    }
}
