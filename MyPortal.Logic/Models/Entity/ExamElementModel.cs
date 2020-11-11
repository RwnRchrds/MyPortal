using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamElementModel : BaseModel
    {
        public Guid BaseElementId { get; set; }
        
        public Guid SeriesId { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }
        
        public decimal? ExamFee { get; set; }

        public bool Submitted { get; set; }

        public virtual ExamBaseElementModel BaseElement { get; set; }
        public virtual ExamSeriesModel Series { get; set; }
    }
}