﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamQualificationLevels")]
    public class ExamQualificationLevel : LookupItem, ISystemEntity
    {
        // TODO: Populate Data

        [Column(Order = 3)]
        public Guid QualificationId { get; set; }

        [Column(Order = 4)]
        public Guid? DefaultGradeSetId { get; set; }

        [Column(Order = 5)]
        [StringLength(25)]
        public string JcLevelCode { get; set; }

        [Column(Order = 6)]
        public bool System { get; set; }

        public virtual GradeSet DefaultGradeSet { get; set; }
        public virtual ExamQualification Qualification { get; set; }
        public virtual ICollection<ExamBaseElement> ExamBaseElements { get; set; }
    }
}
