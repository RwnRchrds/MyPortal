using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Column(Order = 1)] 
        public Guid BaseElementId { get; set; }

        [Column(Order = 2)] 
        public Guid SeriesId { get; set; }

        [Column(Order = 3)]
        [StringLength(256)]
        public string Description { get; set; }

        [Column(Order = 4, TypeName = "decimal(10,2)")]
        public decimal? ExamFee { get; set; }

        public bool Submitted { get; set; }

        public virtual ExamBaseElement BaseElement { get; set; }
        public virtual ExamSeries Series { get; set; }
        public virtual ICollection<ExamAwardElement> ExamAwardElements { get; set; }
        public virtual ICollection<ExamElementComponent> ExamElementComponents { get; set; }
    }
}
