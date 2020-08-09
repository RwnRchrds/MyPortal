using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    public enum ExamAssessmentType
    {
        Award,
        Element,
        Component
    }

    [Table("ExamAssessments")]
    public class ExamAssessment : Entity
    {
        [Column(Order = 1)]
        public Guid ExamBoardId { get; set; }

        [Column(Order = 2)]
        public ExamAssessmentType AssessmentType { get; set; }

        [Column(Order = 3)]
        public string InternalTitle { get; set; }

        [Column(Order = 4)]
        public string ExternalTitle { get; set; }

        public virtual ExamBoard ExamBoard { get; set; }
        public virtual ExamAward ExamAward { get; set; }
        public virtual ExamBaseComponent ExamBaseComponent { get; set; }
        public virtual ExamBaseElement ExamBaseElement { get; set; }
        public virtual ICollection<ExamAssessmentAspect> Aspects { get; set; }
    }
}
