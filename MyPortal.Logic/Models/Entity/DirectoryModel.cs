using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class DirectoryModel : BaseModel
    {
        public Guid? ParentId { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public bool Restricted { get; set; } 

        public virtual DirectoryModel Parent { get; set; }
    }
}
