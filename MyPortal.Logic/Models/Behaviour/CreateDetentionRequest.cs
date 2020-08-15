using System;

namespace MyPortal.Logic.Models.Behaviour
{
    public class CreateDetentionRequest
    {
        public Guid Id { get; set; }

        public Guid DetentionTypeId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Guid? SupervisorId { get; set; }
        public Guid? RoomId { get; set; }
    }
}
