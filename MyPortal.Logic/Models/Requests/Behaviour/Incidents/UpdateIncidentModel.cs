using System;

namespace MyPortal.Logic.Models.Requests.Behaviour.Incidents
{
    public class UpdateIncidentModel : CreateIncidentModel
    {
        public Guid Id { get; set; }
    }
}
