using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamCandidateSeries")]
    public class ExamCandidateSeries : Entity
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
