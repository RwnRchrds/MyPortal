using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("CommunicationLog")]
    public class CommunicationLog
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid PersonId { get; set; }

        [DataMember]
        public Guid ContactId { get; set; }

        [DataMember]
        public Guid CommunicationTypeId { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember] 
        public bool Outgoing { get; set; }

        public virtual CommunicationType Type { get; set; }
    }
}