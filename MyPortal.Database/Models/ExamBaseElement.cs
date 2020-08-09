using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.BaseTypes;

namespace MyPortal.Database.Models
{
    public class ExamBaseElement : Entity
    {
        public ExamBaseElement()
        {
            Elements = new HashSet<ExamElement>();
        }

        [Column(Order = 1)]
        public Guid AssessmentId { get; set; }

        [Column(Order = 2)]
        public Guid LevelId { get; set; }

        [Column(Order = 3)]
        public Guid QcaCodeId { get; set; }

        [Column(Order = 4)]
        public string QualAccrNumber { get; set; }

        [Column(Order = 12)]
        public string ElementCode { get; set; }

        public virtual ExamAssessment Assessment { get; set; }
        public virtual SubjectCode QcaCode { get; set; }
        public virtual ExamQualificationLevel Level { get; set; }
        public virtual ICollection<ExamElement> Elements { get; set; }
    }
}
