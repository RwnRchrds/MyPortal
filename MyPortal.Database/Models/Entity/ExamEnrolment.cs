using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamEnrolments")]
    public class ExamEnrolment : BaseTypes.Entity
    {
        public Guid AwardId { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string RegistrationNumber { get; set; }

        public virtual ExamAward Award { get; set; }
        public virtual ExamCandidate Candidate { get; set; }
    }
}
