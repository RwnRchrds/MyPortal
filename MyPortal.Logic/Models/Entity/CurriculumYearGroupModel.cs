using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class CurriculumYearGroupModel
    {
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        
        public int KeyStage { get; set; }
    }
}