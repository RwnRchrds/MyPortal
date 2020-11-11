using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class SystemAreaModel : BaseModel
    {
        [Required]
        [StringLength(128)]
        public string Description { get; set; }
        
        public Guid? ParentId { get; set; }

        public virtual SystemAreaModel Parent { get; set; }
    }
}