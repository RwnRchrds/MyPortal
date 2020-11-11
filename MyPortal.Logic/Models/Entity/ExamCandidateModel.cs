using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamCandidateModel : BaseModel
    {
        public Guid StudentId { get; set; }

        public string Uci { get; set; }

        public string CandidateNumber { get; set; }

        public string PreviousCandidateNumber { get; set; }

        public string PreviousCentreNumber { get; set; }

        public bool SpecialConsideration { get; set; }

        public string Note { get; set; }
        
        public virtual StudentModel Student { get; set; }
    }
}
