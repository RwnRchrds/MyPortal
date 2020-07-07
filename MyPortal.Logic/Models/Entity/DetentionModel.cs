using System;

namespace MyPortal.Logic.Models.Entity
{
    public class DetentionModel
    {
        public Guid Id { get; set; }
        
        public Guid DetentionTypeId { get; set; }
        
        public Guid EventId { get; set; }
        
        public Guid? SupervisorId { get; set; }

        public virtual DetentionTypeModel Type { get; set; }
        public virtual DiaryEventModel Event { get; set; }
        public virtual StaffMemberModel Supervisor { get; set; }
    }
}