using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamAwardElements")]
    public class ExamAwardElement : Entity
    {
        [Column(Order = 1)]
        public Guid AwardId { get; set; }

        [Column(Order = 2)]
        public Guid ElementId { get; set; }

        public virtual ExamAward Award { get; set; }
        public virtual ExamElement Element { get; set; }
    }
}
