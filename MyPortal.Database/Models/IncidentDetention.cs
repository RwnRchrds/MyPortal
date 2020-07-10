using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("IncidentDetention")]
    public class IncidentDetention : IEntity
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid IncidentId { get; set; }

        [Column(Order = 2)]
        public Guid DetentionId { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Detention Detention { get; set; }
    }
}
