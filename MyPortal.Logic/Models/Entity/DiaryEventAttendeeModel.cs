using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class DiaryEventAttendeeModel : BaseModel
    {
        public Guid EventId { get; set; }
        
        public Guid PersonId { get; set; }
        
        public Guid? ResponseId { get; set; }
        
        public bool Required { get; set; }
        
        public bool Attended { get; set; }

        public virtual DiaryEventModel Event { get; set; }
        public virtual PersonModel Person { get; set; }
        public virtual DiaryEventAttendeeResponseModel Response { get; set; }
    }
}