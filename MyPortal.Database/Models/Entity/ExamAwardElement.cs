using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamAwardElements")]
    public class ExamAwardElement : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid AwardId { get; set; }

        [Column(Order = 2)]
        public Guid ElementId { get; set; }

        public virtual ExamAward Award { get; set; }
        public virtual ExamElement Element { get; set; }
    }
}
