﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamBoards")]
    public class ExamBoard : BaseTypes.Entity, IActivatable
    {
        // TODO: Populate Data

        [Column(Order = 2)] [StringLength(20)] public string Abbreviation { get; set; }

        [Column(Order = 3)]
        [StringLength(128)]
        public string FullName { get; set; }

        [Column(Order = 4)] [StringLength(5)] public string Code { get; set; }

        [Column(Order = 5)] public bool Domestic { get; set; }

        [Column(Order = 6)] public bool UseEdi { get; set; }

        [Column(Order = 7)] public bool Active { get; set; }

        public virtual ICollection<ExamSeries> ExamSeries { get; set; }
        public virtual ICollection<ExamAssessment> ExamAssessments { get; set; }
    }
}