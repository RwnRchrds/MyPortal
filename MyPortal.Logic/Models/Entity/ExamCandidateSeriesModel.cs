using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamCandidateSeriesModel : BaseModel
    {
        public Guid SeriesId { get; set; }
        
        public Guid CandidateId { get; set; }
        
        public string Flag { get; set; }
        
        public virtual ExamSeriesModel Series { get; set; }
        public virtual ExamCandidateModel Candidate { get; set; }
    }
}