using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamResultsEmbargoes")]
    public class ExamResultsEmbargo : Entity
    {
        [Column(Order = 1)]
        public Guid ExamSeasonId { get; set; }

        [Column(Order = 2)]
        public DateTime StartTime { get; set; }

        [Column(Order = 3)]
        public DateTime EndTime { get; set; }

        public virtual ExamSeason ExamSeason { get; set; }
    }
}
