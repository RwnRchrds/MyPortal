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
        public Guid ExamSeriesId { get; set; }

        [Column(Order = 2)]
        public Guid QualificationId { get; set; }

        [Column(Order = 3)]
        public string ExternalTitle { get; set; }

        [Column(Order = 4)]
        public string InternalTitle { get; set; }

        [Column(Order = 5)]
        public string Description { get; set; }

        [Column(Order = 6)]
        public string AwardCode { get; set; }

        public virtual ExamSeries Series { get; set; }
        public virtual ExamQualification Qualification { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<ExamElement> Elements { get; set; }
    }
}
