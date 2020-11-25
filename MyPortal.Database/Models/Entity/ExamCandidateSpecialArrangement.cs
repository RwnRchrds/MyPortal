using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamCandidateSpecialArrangements")]
    public class ExamCandidateSpecialArrangement : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid CandidateId { get; set; }

        [Column(Order = 2)]
        public Guid SpecialArrangementId { get; set; }

        public virtual ExamCandidate Candidate { get; set; }
        public virtual ExamSpecialArrangement SpecialArrangement { get; set; }
    }
}
