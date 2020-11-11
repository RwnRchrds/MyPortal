using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamAwardElementModel : BaseModel
    {
        public Guid AwardId { get; set; }
        
        public Guid ElementId { get; set; }

        public virtual ExamAwardModel Award { get; set; }
        public virtual ExamElementModel Element { get; set; }
    }
}