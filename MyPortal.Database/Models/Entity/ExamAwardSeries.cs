using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamAwardSeries")]
    public class ExamAwardSeries : BaseTypes.Entity
    {
        public Guid AwardId { get; set; }
        public Guid SeriesId { get; set; }

        public virtual ExamAward Award { get; set; }
        public virtual ExamSeries Series { get; set; }
    }
}
