using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamEnrolmentModel : BaseModel
    {
        public Guid AwardId { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string RegistrationNumber { get; set; }

        public virtual ExamAwardModel Award { get; set; }
        public virtual ExamCandidateModel Candidate { get; set; }
    }
}