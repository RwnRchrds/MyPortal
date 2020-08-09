using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    public class ExamSpecialArrangement : Entity
    {
        [Column(Order = 1)]
        public string Description { get; set; }

        [Column(Order = 2)]
        public bool ExtraTime { get; set; }

        public virtual ICollection<ExamCandidateSpecialArrangement> Candidates { get; set; }
    }
}
