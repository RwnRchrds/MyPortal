using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class SenEventModel : BaseModel
    {
        public Guid StudentId { get; set; }
        
        public Guid EventTypeId { get; set; }
        
        public DateTime Date { get; set; }
        
        [Required]
        public string Note { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual SenEventTypeModel Type { get; set; }
    }
}