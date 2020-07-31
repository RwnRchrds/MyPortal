using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("ExamElements")]
    public class ExamElement : Entity
    {
        public ExamElement()
        {
            Components = new HashSet<ExamComponent>();
        }

        [Column(Order = 1)]
        public Guid ExamAwardId { get; set; }

        [Column(Order = 2)]
        public Guid QcaCodeId { get; set; }

        [Column(Order = 3)]
        public string Qan { get; set; }

        [Column(Order = 4)]
        public Guid LevelId { get; set; }

        [Column(Order = 5)]
        public Guid EntryAspectId { get; set; }

        [Column(Order = 6)]
        public Guid ResultAspectId { get; set; }

        [Column(Order = 7, TypeName = "decimal(10, 2)")]
        public decimal Fees { get; set; }

        [Column(Order = 8)]
        public string InternalTitle { get; set; }

        [Column(Order = 9)]
        public string ExternalTitle { get; set; }

        [Column(Order = 10)]
        public string Description { get; set; }

        [Column(Order = 11)]
        public string EntryCode { get; set; }

        public virtual SubjectCode QcaCode { get; set; }
        public virtual ExamAward Award { get; set; }
        public virtual Aspect EntryAspect { get; set; }
        public virtual Aspect ResultAspect { get; set; }
        public virtual ICollection<ExamComponent> Components { get; set; }
    }
}
