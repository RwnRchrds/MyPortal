using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class DetentionDto
    {
        public Guid Id { get; set; }
        public Guid DetentionTypeId { get; set; }
        public Guid EventId { get; set; }
        public Guid? SupervisorId { get; set; }

        public virtual DetentionTypeDto Type { get; set; }
        public virtual DiaryEventDto Event { get; set; }
        public virtual StaffMemberDto Supervisor { get; set; }
    }
}
