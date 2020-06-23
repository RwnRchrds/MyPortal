using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("CommunicationLog")]
    public class CommunicationLog
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid PersonId { get; set; }

        [Column(Order = 2)]
        public Guid ContactId { get; set; }

        [Column(Order = 3)]
        public Guid CommunicationTypeId { get; set; }

        [Column(Order = 4)]
        public DateTime Date { get; set; }

        [Column(Order = 5)]
        public string Note { get; set; }

        [Column(Order = 6)] 
        public bool Outgoing { get; set; }

        public virtual CommunicationType Type { get; set; }
    }
}