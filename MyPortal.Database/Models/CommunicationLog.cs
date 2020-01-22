using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("CommunicationLog")]
    public class CommunicationLog
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int CommunicationTypeId { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public virtual CommunicationType Type { get; set; }
    }
}