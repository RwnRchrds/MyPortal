using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    public class ExamElementComponent : Entity
    {
        public Guid ElementId { get; set; }
        public Guid ComponentId { get; set; }

        public virtual ExamElement Element { get; set; }
        public virtual ExamComponent Component { get; set; }
    }
}
