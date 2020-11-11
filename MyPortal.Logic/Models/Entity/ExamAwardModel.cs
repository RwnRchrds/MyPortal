using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamAwardModel : BaseModel
    {
        public Guid QualificationId { get; set; }
        
        public Guid AssessmentId { get; set; }
        
        public Guid? CourseId { get; set; }
        
        public string Description { get; set; }
        
        public string AwardCode { get; set; }
        
        public DateTime? ExpiryDate { get; set; }

        public virtual ExamAssessmentModel Assessment { get; set; }
        public virtual ExamQualificationModel Qualification { get; set; }
        public virtual CourseModel Course { get; set; }
    }
}