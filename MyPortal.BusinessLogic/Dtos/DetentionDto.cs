using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class DetentionDto
    {
        public int Id { get; set; }
        public int DetentionTypeId { get; set; }
        public int EventId { get; set; }
        public int SupervisorId { get; set; }

        public virtual StaffMemberDto Supervisor { get; set; }
        public virtual DiaryEventDto Event { get; set; }
        public virtual DetentionTypeDto Type { get; set; }
    }
}
