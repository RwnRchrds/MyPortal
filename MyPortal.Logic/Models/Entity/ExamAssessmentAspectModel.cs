using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamAssessmentAspectModel : BaseModel
    {
        public Guid AssessmentId { get; set; }
        
        public Guid AspectId { get; set; }
        
        public Guid SeriesId { get; set; }
        
        public int AspectOrder { get; set; }

        public virtual AspectModel Aspect { get; set; }
        public virtual ExamAssessmentModel Assessment { get; set; }
        public virtual ExamSeriesModel Series { get; set; }
    }
}