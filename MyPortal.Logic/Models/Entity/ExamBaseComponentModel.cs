using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamBaseComponentModel : BaseModel
    {
        public Guid AssessmentModeId { get; set; }
        
        public Guid ExamAssessmentId { get; set; }
        
        public string ComponentCode { get; set; }

        public virtual ExamAssessmentModeModel AssessmentMode { get; set; }
        public virtual ExamAssessmentModel Assessment { get; set; }
    }
}