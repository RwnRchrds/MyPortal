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
        public Guid ExamElementId { get; set; }

        [Column(Order = 2)]
        public Guid AssessmentModeId { get; set; }

        [Column(Order = 3)]
        public Guid AspectId { get; set; }

        [Column(Order = 4)]
        public string InternalTitle { get; set; }

        [Column(Order = 5)]
        public string ExternalTitle { get; set; }

        [Column(Order = 6)]
        public string ComponentCode { get; set; }

        [Column(Order = 7)]
        public DateTime? DateDue { get; set; }

        [Column(Order = 8)]
        public DateTime? DateSubmitted { get; set; }

        [Column(Order = 9)]
        public bool IsTimetabled { get; set; }

        [Column(Order = 10)]
        public int MaximumMark { get; set; }
        
        #region Timetable

        [Column(Order = 11)]
        public DateTime? ExaminationDate { get; set; }

        [Column(Order = 12)]
        public Guid? ExamSessionId { get; set; }

        [Column(Order = 13)]
        public DateTime? StartTime { get; set; }

        [Column(Order = 14)]
        public int? Duration { get; set; }

        #endregion

        public virtual ExamElement Element { get; set; }
        public virtual ExamAssessmentMode AssessmentMode { get; set; }
        public virtual Aspect Aspect { get; set; }
        public virtual ExamSession Session { get; set; }
    }
}
