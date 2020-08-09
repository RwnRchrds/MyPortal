using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("ExamSeries")]
    public class ExamSeries : Entity
    {
        [Column(Order = 1)]
        public Guid ExamBoardId { get; set; }

        [Column(Order = 2)]
        public Guid ExamSeasonId { get; set; }

        [Column(Order = 3)]
        public string SeriesCode { get; set; }

        [Column(Order = 4)]
        public string Code { get; set; }

        [Column(Order = 5)]
        public string Title { get; set; }

        public virtual ExamSeason Season { get; set; }
        public virtual ExamBoard ExamBoard { get; set; }
        public virtual ICollection<ExamElement> ExamElements { get; set; }
        public virtual ICollection<ExamComponent> ExamComponents { get; set; }
        public virtual ICollection<ExamAssessmentAspect> ExamAssessmentAspects { get; set; }
        public virtual ICollection<ExamAwardSeries> ExamAwardSeries { get; set; }
        public virtual ICollection<ExamCandidateSeries> ExamCandidateSeries { get; set; }
    }
}
