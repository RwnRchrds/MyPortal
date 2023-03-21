using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamAwardSeries")]
    public class ExamAwardSeries : BaseTypes.Entity
    {
        [Column(Order = 2)]
        public Guid AwardId { get; set; }
        
        [Column(Order = 3)]
        public Guid SeriesId { get; set; }

        public virtual ExamAward Award { get; set; }
        public virtual ExamSeries Series { get; set; }
    }
}
