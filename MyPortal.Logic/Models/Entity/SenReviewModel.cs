using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class SenReviewModel : BaseModel
    {
        public Guid StudentId { get; set; }
        
        public Guid ReviewTypeId { get; set; }
        
        public DateTime Date { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }
        
        [StringLength(256)]
        public string Outcome { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual SenReviewTypeModel ReviewType { get; set; }
    }
}