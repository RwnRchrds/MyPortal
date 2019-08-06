using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    /// <summary>
    /// Record of communication made between the school and a contact.
    /// </summary>
    [Table("Communication_CommunicationLogs")]
    public class CommunicationLog
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int CommunicationTypeId { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public virtual CommunicationType CommunicationType { get; set; }
    }
}