using System;

namespace MyPortal.Logic.Models.Requests.Behaviour.Incidents
{
    public class UpdateIncidentRequestModel : CreateIncidentRequestModel
    {
        public Guid Id { get; set; }
    }
}
