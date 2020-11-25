using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("CurriculumBlocks")]
    public class CurriculumBlock : BaseTypes.Entity
    {
        public CurriculumBlock()
        {
            Groups = new HashSet<CurriculumGroup>();
            BandAssignments = new HashSet<CurriculumBandBlockAssignment>();
        }

        [Column(Order = 1)]
        [StringLength(10)]
        public string Code { get; set; }
        
        [Column(Order = 2)]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<CurriculumGroup> Groups { get; set; }
        public virtual ICollection<CurriculumBandBlockAssignment> BandAssignments { get; set; }
    }
}