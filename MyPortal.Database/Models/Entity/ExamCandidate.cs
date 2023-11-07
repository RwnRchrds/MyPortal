using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamCandidate")]
    public class ExamCandidate : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid StudentId { get; set; }

        [Column(Order = 3)] public string Uci { get; set; }

        [Column(Order = 4)] [StringLength(4)] public string CandidateNumber { get; set; }

        [Column(Order = 5)] [StringLength(4)] public string PreviousCandidateNumber { get; set; }

        [Column(Order = 6)] [StringLength(5)] public string PreviousCentreNumber { get; set; }

        [Column(Order = 7)] public bool SpecialConsideration { get; set; }

        [Column(Order = 8)] public string Note { get; set; }

        public virtual Student Student { get; set; }
        public virtual ICollection<ExamCandidateSeries> LinkedSeries { get; set; }
        public virtual ICollection<ExamCandidateSpecialArrangement> SpecialArrangements { get; set; }
        public virtual ICollection<ExamSeatAllocation> SeatAllocations { get; set; }
        public virtual ICollection<ExamEnrolment> ExamEnrolments { get; set; }
    }
}