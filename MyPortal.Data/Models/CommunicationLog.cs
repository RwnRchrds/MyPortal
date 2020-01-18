using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Data.Models
{
    /// <summary>
    /// Record of communication made between the school and a contact.
    /// </summary>
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