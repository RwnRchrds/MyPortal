using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamComponentModel : BaseModel
    {
        public Guid BaseComponentId { get; set; }
        
        public Guid ExamSeriesId { get; set; }
        
        public Guid AssessmentModeId { get; set; }

        
        public DateTime? DateDue { get; set; }

        
        public DateTime? DateSubmitted { get; set; }

        
        public bool IsTimetabled { get; set; }

        
        public int MaximumMark { get; set; }
        
        #region Examination Specific Data

        
        public Guid? SessionId { get; set; }

        
        public int? Duration { get; set; }

        
        public DateTime? SittingDate { get; set; }

        
        public Guid? ExamSessionId { get; set; }

        #endregion

        public virtual ExamBaseComponentModel BaseComponent { get; set; }
        public virtual ExamSeriesModel Series { get; set; }
        public virtual ExamAssessmentModeModel AssessmentMode { get; set; }
        public virtual ExamSessionModel Session { get; set; }
    }
}