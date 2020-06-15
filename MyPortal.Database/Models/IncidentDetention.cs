using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("IncidentDetention")]
    public class IncidentDetention
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid IncidentId { get; set; }

        [DataMember]
        public Guid DetentionId { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Detention Detention { get; set; }
    }
}
