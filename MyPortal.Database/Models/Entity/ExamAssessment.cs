using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    public enum ExamAssessmentType
    {
        Award,
        Element,
        Component
    }

    [Table("ExamAssessments")]
    public class ExamAssessment : BaseTypes.Entity
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
