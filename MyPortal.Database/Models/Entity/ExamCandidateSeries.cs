using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamCandidateSeries")]
    public class ExamCandidateSeries : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid SeriesId { get; set; }

        [Column(Order = 2)]
        public Guid CandidateId { get; set; }

        [Column(Order = 3)]
        public string Flag { get; set; }

        public virtual ExamSeries Series { get; set; }
        public virtual ExamCandidate Candidate { get; set; }
    }
}
