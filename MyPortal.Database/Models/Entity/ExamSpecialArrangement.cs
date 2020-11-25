using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    public class ExamSpecialArrangement : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public string Description { get; set; }

        [Column(Order = 2)]
        public bool ExtraTime { get; set; }

        public virtual ICollection<ExamCandidateSpecialArrangement> Candidates { get; set; }
    }
}
