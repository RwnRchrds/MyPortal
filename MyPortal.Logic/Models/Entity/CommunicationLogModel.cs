using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class CommunicationLogModel : BaseModel
    {
        public Guid PersonId { get; set; }
        
        public Guid ContactId { get; set; }
        
        public Guid CommunicationTypeId { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Note { get; set; }
        
        public bool Outgoing { get; set; }

        public virtual CommunicationTypeModel Type { get; set; }
    }
}