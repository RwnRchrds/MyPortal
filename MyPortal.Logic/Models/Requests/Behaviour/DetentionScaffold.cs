using System;

namespace MyPortal.Logic.Models.Requests.Behaviour
{
    public class DetentionScaffold
    {
        public Guid DetentionId { get; set; }
        public Guid DetentionTypeId { get; set; }
        public Guid? SupervisorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}