using System;

namespace MyPortal.Database.Models.Entity
{
    public class ExamElementComponent : BaseTypes.Entity
    {
        public Guid ElementId { get; set; }
        public Guid ComponentId { get; set; }

        public virtual ExamElement Element { get; set; }
        public virtual ExamComponent Component { get; set; }
    }
}
