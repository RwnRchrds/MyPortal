using System;
using MyPortal.Logic.Enums;

namespace MyPortal.Logic.Models.Requests.Behaviour.Detentions
{
    public class DetentionRequestModel
    {
        public Guid DetentionTypeId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Guid? SupervisorId { get; set; }
        public Guid? RoomId { get; set; }

        public EventFrequency Frequency { get; set; }
        public DateTime? LastOccurrence { get; set; }
        public bool SameSupervisor { get; set; }
    }
}