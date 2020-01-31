using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Models.Dtos
{
    public class IncidentDetentionDto
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public int DetentionId { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Detention Detention { get; set; }
    }
}
