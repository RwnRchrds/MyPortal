using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    public class ExamElementComponent : BaseTypes.Entity
    {
        [Column(Order = 2)]
        public Guid ElementId { get; set; }
        
        [Column(Order = 3)]
        public Guid ComponentId { get; set; }

        public virtual ExamElement Element { get; set; }
        public virtual ExamComponent Component { get; set; }
    }
}
