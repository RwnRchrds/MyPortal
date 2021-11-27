using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamDates")]
    public class ExamDate : BaseTypes.Entity
    {
        [Column(Order = 2)]
        public Guid SessionId { get; set; }

        [Column(Order = 3)]
        public int Duration { get; set; }

        [Column(Order = 4)]
        public DateTime SittingDate { get; set; }

        public virtual ExamSession Session { get; set; }
        public virtual ICollection<ExamComponent> ExamComponents { get; set; }
    }
}