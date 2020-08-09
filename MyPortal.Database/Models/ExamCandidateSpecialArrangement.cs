using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamCandidateSpecialArrangements")]
    public class ExamCandidateSpecialArrangement : Entity
    {
        [Column(Order = 1)]
        public Guid CandidateId { get; set; }

        [Column(Order = 2)]
        public Guid SpecialArrangementId { get; set; }

        public virtual ExamCandidate Candidate { get; set; }
        public virtual ExamSpecialArrangement SpecialArrangement { get; set; }
    }
}
