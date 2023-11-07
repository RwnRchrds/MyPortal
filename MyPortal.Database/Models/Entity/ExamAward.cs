using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamAwards")]
    public class ExamAward : BaseTypes.Entity
    {
        [Column(Order = 2)] public Guid QualificationId { get; set; }

        [Column(Order = 3)] public Guid AssessmentId { get; set; }

        [Column(Order = 4)] public Guid? CourseId { get; set; }

        [Column(Order = 5)] public string Description { get; set; }

        [Column(Order = 6)] public string AwardCode { get; set; }

        [Column(Order = 7, TypeName = "date")] public DateTime? ExpiryDate { get; set; }

        public virtual ExamAssessment Assessment { get; set; }
        public virtual ExamQualification Qualification { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<ExamAwardElement> ExamAwardElements { get; set; }
        public virtual ICollection<ExamAwardSeries> ExamAwardSeries { get; set; }
        public virtual ICollection<ExamEnrolment> ExamEnrolments { get; set; }
    }
}