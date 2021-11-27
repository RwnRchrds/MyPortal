using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamComponents")]
    public class ExamComponent : BaseTypes.Entity
    {
        [Column(Order = 1)]
        public Guid BaseComponentId { get; set; }

        [Column(Order = 2)] 
        public Guid ExamSeriesId { get; set; }

        [Column(Order = 3)] 
        public Guid AssessmentModeId { get; set; }
        
        // Components can be COURSEWORK or EXAMINATIONS
        [Column(Order = 4)] 
        public Guid? ExamDateId { get; set; }

        [Column(Order = 5)]
        public DateTime? DateDue { get; set; }

        [Column(Order = 6)]
        public DateTime? DateSubmitted { get; set; }

        [Column(Order = 7)]
        public int MaximumMark { get; set; }

        public virtual ExamBaseComponent BaseComponent { get; set; }
        public virtual ExamSeries Series { get; set; }
        public virtual ExamAssessmentMode AssessmentMode { get; set; }
        
        public virtual ExamDate ExamDate { get; set; }

        public virtual ICollection<ExamComponentSitting> Sittings { get; set; }
        public virtual ICollection<ExamElementComponent> ExamElementComponents { get; set; }
    }
}
