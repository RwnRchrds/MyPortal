using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamCandidateSpecialArrangementModel : BaseModel
    {
        public Guid CandidateId { get; set; }
        
        public Guid SpecialArrangementId { get; set; }

        public virtual ExamCandidateModel Candidate { get; set; }
        public virtual ExamSpecialArrangementModel SpecialArrangement { get; set; }
    }
}