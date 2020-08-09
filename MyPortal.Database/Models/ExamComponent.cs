using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    [Table("ExamComponents")]
    public class ExamComponent : Entity
    {
        [Column(Order = 1)]
        public Guid BaseComponentId { get; set; }

        [Column(Order = 2)] 
        public Guid ExamSeriesId { get; set; }

        [Column(Order = 3)] 
        public Guid AssessmentModeId { get; set; }

        [Column(Order = 4)]
        public DateTime? DateDue { get; set; }

        [Column(Order = 5)]
        public DateTime? DateSubmitted { get; set; }

        [Column(Order = 6)]
        public bool IsTimetabled { get; set; }

        [Column(Order = 7)]
        public int MaximumMark { get; set; }

        // Components can be COURSEWORK or EXAMINATIONS
        #region Examination Specific Data

        [Column(Order = 8)]
        public Guid? SessionId { get; set; }

        [Column(Order = 9)]
        public int? Duration { get; set; }

        [Column(Order = 10)]
        public DateTime? SittingDate { get; set; }

        [Column(Order = 11)]
        public Guid? ExamSessionId { get; set; }

        #endregion

        public virtual ExamBaseComponent BaseComponent { get; set; }
        public virtual ExamSeries Series { get; set; }
        public virtual ExamAssessmentMode AssessmentMode { get; set; }
        public virtual ExamSession Session { get; set; }

        public virtual ICollection<ExamComponentSitting> Sittings { get; set; }
        public virtual ICollection<ExamElementComponent> ExamElementComponents { get; set; }
    }
}
