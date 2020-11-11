using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class CurriculumYearGroupModel : BaseModel
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        
        public int KeyStage { get; set; }
        
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
    }
}