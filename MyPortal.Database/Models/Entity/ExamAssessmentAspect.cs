using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    public class ExamAssessmentAspect : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid AssessmentId { get; set; }

        [Column(Order = 2)]
        public Guid AspectId { get; set; }

        [Column(Order = 3)]
        public Guid SeriesId { get; set; }

        [Column(Order = 4)]
        public int AspectOrder { get; set; }

        public virtual Aspect Aspect { get; set; }
        public virtual ExamAssessment Assessment { get; set; }
        public virtual ExamSeries Series { get; set; }
    }
}
