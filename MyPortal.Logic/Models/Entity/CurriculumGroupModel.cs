using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class CurriculumGroupModel
    {
        public Guid Id { get; set; }
        
        public Guid BlockId { get; set; }
        
        [StringLength(10)]
        public string Code { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }

        public virtual CurriculumBlockModel Block { get; set; }
    }
}