using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamBaseComponents")]
    public class ExamBaseComponent : BaseTypes.Entity
    {
        [Column(Order = 2)]
        public Guid AssessmentModeId { get; set; }

        [Column(Order = 3)]
        public Guid ExamAssessmentId { get; set; }
        
        [Column(Order = 4)]
        public string ComponentCode { get; set; }

        public virtual ExamAssessmentMode AssessmentMode { get; set; }
        public virtual ExamAssessment Assessment { get; set; }
        public virtual ICollection<ExamComponent> ExamComponents { get; set; }
    }
}
