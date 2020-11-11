using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class CurriculumBandModel : BaseModel
    {
        public Guid AcademicYearId { get; set; }
        
        public Guid CurriculumYearGroupId { get; set; }
        
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }

        public virtual AcademicYearModel AcademicYear { get; set; }
        public virtual CurriculumYearGroupModel CurriculumYearGroup { get; set; }
    }
}