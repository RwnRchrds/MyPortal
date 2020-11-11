using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class IncidentDetentionModel : BaseModel
    {
        public Guid IncidentId { get; set; }

        public Guid DetentionId { get; set; }

        public virtual IncidentModel Incident { get; set; }
        public virtual DetentionModel Detention { get; set; }
    }
}
