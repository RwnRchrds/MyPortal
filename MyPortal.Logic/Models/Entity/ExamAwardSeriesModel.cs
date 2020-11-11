using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamAwardSeriesModel : BaseModel
    {
        public Guid AwardId { get; set; }
        public Guid SeriesId { get; set; }

        public virtual ExamAwardModel Award { get; set; }
        public virtual ExamSeriesModel Series { get; set; }
    }
}