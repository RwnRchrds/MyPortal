using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamSeasons")]
    public class ExamSeason : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid ResultSetId { get; set; }

        [Column(Order = 2)]
        public int CalendarYear { get; set; }

        [Column(Order = 3)]
        public DateTime StartDate { get; set; }

        [Column(Order = 4)]
        public DateTime EndDate { get; set; }

        [Column(Order = 5)]
        public string Name { get; set; }

        [Column(Order = 6)]
        public string Description { get; set; }

        [Column(Order = 7)]
        public bool Default { get; set; }

        public virtual ResultSet ResultSet { get; set; }
        public virtual ICollection<ExamSeries> ExamSeries { get; set; }
    }
}
