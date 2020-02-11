using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Models.Dtos
{
    public class IncidentDetentionDto
    {
        public Guid Id { get; set; }
        public Guid IncidentId { get; set; }
        public Guid DetentionId { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Detention Detention { get; set; }
    }
}
