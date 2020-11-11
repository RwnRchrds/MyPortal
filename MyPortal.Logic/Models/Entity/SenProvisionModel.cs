using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class SenProvisionModel : BaseModel
    {
        public Guid StudentId { get; set; }
        
        public Guid ProvisionTypeId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        [Required]
        public string Note { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual SenProvisionTypeModel Type { get; set; }
    }
}