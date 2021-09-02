using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    public class ExamSpecialArrangement : BaseTypes.Entity, ISystemEntity
    {
        [Column(Order = 1)]
        public string Description { get; set; }

        [Column(Order = 2)]
        public bool System { get; set; }

        public virtual ICollection<ExamCandidateSpecialArrangement> Candidates { get; set; }
    }
}
