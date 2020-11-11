using System;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class CurriculumBandBlockAssignmentModel : BaseModel
    {
        public Guid BlockId { get; set; }
        
        public Guid BandId { get; set; }
        
        public virtual CurriculumBlockModel Block { get; set; }
        public virtual CurriculumBandModel Band { get; set; }
    }
}