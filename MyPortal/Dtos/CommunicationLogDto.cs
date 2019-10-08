using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    /// <summary>
    /// Record of communication made between the school and a contact.
    /// </summary>
    
    public class CommunicationLogDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int CommunicationTypeId { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public virtual CommunicationTypeDto CommunicationType { get; set; }
    }
}