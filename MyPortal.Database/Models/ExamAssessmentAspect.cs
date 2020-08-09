using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    public class ExamAssessmentAspect : Entity
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
