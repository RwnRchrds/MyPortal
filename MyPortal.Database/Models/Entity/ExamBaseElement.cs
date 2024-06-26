﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    public class ExamBaseElement : BaseTypes.Entity
    {
        public ExamBaseElement()
        {
            Elements = new HashSet<ExamElement>();
        }

        [Column(Order = 2)] public Guid AssessmentId { get; set; }

        [Column(Order = 3)] public Guid LevelId { get; set; }

        [Column(Order = 4)] public Guid QcaCodeId { get; set; }

        [Column(Order = 5)] public string QualAccrNumber { get; set; }

        [Column(Order = 6)] public string ElementCode { get; set; }

        public virtual ExamAssessment Assessment { get; set; }
        public virtual SubjectCode QcaCode { get; set; }
        public virtual ExamQualificationLevel Level { get; set; }
        public virtual ICollection<ExamElement> Elements { get; set; }
    }
}