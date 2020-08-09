using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamAwardSeries")]
    public class ExamAwardSeries : Entity
    {
        public Guid AwardId { get; set; }
        public Guid SeriesId { get; set; }

        public virtual ExamAward Award { get; set; }
        public virtual ExamSeries Series { get; set; }
    }
}
