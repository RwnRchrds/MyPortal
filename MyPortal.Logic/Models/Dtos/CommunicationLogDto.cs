using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class CommunicationLogDto
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Guid CommunicationTypeId { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public virtual CommunicationTypeDto Type { get; set; }
    }
}
