using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamBaseElementModel : BaseModel
    {
        public Guid AssessmentId { get; set; }
        
        public Guid LevelId { get; set; }
        
        public Guid QcaCodeId { get; set; }
        
        public string QualAccrNumber { get; set; }
        
        public string ElementCode { get; set; }

        public virtual ExamAssessmentModel Assessment { get; set; }
        public virtual SubjectCodeModel QcaCode { get; set; }
        public virtual ExamQualificationLevelModel Level { get; set; }
    }
}