using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class DiaryEventAttendeeDto
    {
        public int EventId { get; set; }
        public int PersonId { get; set; }
        public int ResponseId { get; set; }
        public bool Required { get; set; }
        public bool Attended { get; set; }

        public virtual DiaryEventDto Event { get; set; }
        public virtual PersonDto Person { get; set; }
        public virtual DiaryEventInvitationResponseDto Response { get; set; }
    }
}
