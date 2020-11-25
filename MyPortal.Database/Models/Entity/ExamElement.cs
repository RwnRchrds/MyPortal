using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("ExamElements")]
    public class ExamElement : BaseTypes.Entity
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
