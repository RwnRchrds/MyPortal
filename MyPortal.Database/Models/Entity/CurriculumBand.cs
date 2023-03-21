using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("CurriculumBands")]
    public class CurriculumBand : BaseTypes.Entity

    {
    public CurriculumBand()
    {
        AssignedBlocks = new HashSet<CurriculumBandBlockAssignment>();
    }

    [Column(Order = 2)] 
    public Guid AcademicYearId { get; set; }

    [Column(Order = 3)] 
    public Guid CurriculumYearGroupId { get; set; }
    
    [Column(Order = 4)] 
    public Guid StudentGroupId { get; set; }

    public virtual AcademicYear AcademicYear { get; set; }
    public virtual CurriculumYearGroup CurriculumYearGroup { get; set; }
    public virtual StudentGroup StudentGroup { get; set; }
    public virtual ICollection<CurriculumBandBlockAssignment> AssignedBlocks { get; set; }
    }
}