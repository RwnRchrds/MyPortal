using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    public class ExamSpecialArrangement : BaseTypes.Entity, ISystemEntity
    {
        [Column(Order = 2)] public string Description { get; set; }

        [Column(Order = 3)] public bool System { get; set; }

        public virtual ICollection<ExamCandidateSpecialArrangement> Candidates { get; set; }
    }
}