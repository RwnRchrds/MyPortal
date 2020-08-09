using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("ExamAwards")]
    public class ExamAward : Entity
    {
        [Column(Order = 1)]
        public Guid QualificationId { get; set; }

        [Column(Order = 2)] 
        public Guid AssessmentId { get; set; }

        [Column(Order = 3)] 
        public Guid? CourseId { get; set; }

        [Column(Order = 6)]
        public string Description { get; set; }

        [Column(Order = 7)]
        public string AwardCode { get; set; }

        [Column(Order = 8)] 
        public DateTime? ExpiryDate { get; set; }

        public virtual ExamAssessment Assessment { get; set; }
        public virtual ExamQualification Qualification { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<ExamAwardElement> ExamAwardElements { get; set; }
        public virtual ICollection<ExamAwardSeries> ExamAwardSeries { get; set; }
        public virtual ICollection<ExamEnrolment> ExamEnrolments { get; set; }
    }
}
