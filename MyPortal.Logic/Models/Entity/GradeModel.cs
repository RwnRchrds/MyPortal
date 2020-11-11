using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class GradeModel : BaseModel
    {
        public Guid GradeSetId { get; set; }
        
        [Required]
        [StringLength(25)]
        public string Code { get; set; }
        
        [StringLength(50)]
        public string Description { get; set; }
        
        public decimal Value { get; set; }

        public virtual GradeSetModel GradeSet { get; set; }
    }
}
