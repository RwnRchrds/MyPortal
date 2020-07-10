using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Entity
{
    public class CurriculumBandModel
    {
        public Guid Id { get; set; }
        
        public Guid AcademicYearId { get; set; }
        
        public Guid CurriculumYearGroupId { get; set; }
        
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }

        public virtual CurriculumYearGroupModel CurriculumYearGroup { get; set; }
    }
}