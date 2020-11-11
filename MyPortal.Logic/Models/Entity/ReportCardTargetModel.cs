using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ReportCardTargetModel : BaseModel
    {
        public Guid ReportCardId { get; set; }
        
        public Guid TargetId { get; set; }

        public virtual ReportCardModel ReportCard { get; set; }
        public virtual BehaviourTargetModel Target { get; set; }
    }
}