using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamBaseComponents")]
    public class ExamBaseComponent : Entity
    {
        [Column(Order = 1)]
        public Guid AssessmentModeId { get; set; }

        [Column(Order = 2)]
        public Guid ExamAssessmentId { get; set; }
        
        [Column(Order = 3)]
        public string ComponentCode { get; set; }

        public virtual ExamAssessmentMode AssessmentMode { get; set; }
        public virtual ExamAssessment Assessment { get; set; }
        public virtual ICollection<ExamComponent> ExamComponents { get; set; }
    }
}
