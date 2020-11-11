using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamElementComponentModel : BaseModel
    {
        public Guid ElementId { get; set; }
        public Guid ComponentId { get; set; }

        public virtual ExamElementModel Element { get; set; }
        public virtual ExamComponentModel Component { get; set; }
    }
}