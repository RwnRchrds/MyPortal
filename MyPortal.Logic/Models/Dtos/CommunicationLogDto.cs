using System;

namespace MyPortal.Logic.Models.Dtos
{
    public class CommunicationLogDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int CommunicationTypeId { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public virtual CommunicationTypeDto Type { get; set; }
    }
}
