using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamEnrolments")]
    public class ExamEnrolment : Entity
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
