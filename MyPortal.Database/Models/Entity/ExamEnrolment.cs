using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamEnrolments")]
    public class ExamEnrolment : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid AwardId { get; set; }

        [Column(Order = 3)] public Guid CandidateId { get; set; }

        [Column(Order = 4)] public DateTime StartDate { get; set; }

        [Column(Order = 5)] public DateTime? EndDate { get; set; }

        [Column(Order = 6)] public string RegistrationNumber { get; set; }

        public virtual ExamAward Award { get; set; }
        public virtual ExamCandidate Candidate { get; set; }
    }
}