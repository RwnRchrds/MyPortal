using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("CurriculumBlock")]
    public class CurriculumBlock
    {
        public CurriculumBlock()
        {
            Groups = new HashSet<CurriculumGroup>();
            BandAssignments = new HashSet<CurriculumBandBlock>();
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid Id { get; set; }
        
        [Column(Order = 1)]
        [StringLength(10)]
        public string Code { get; set; }
        
        [Column(Order = 2)]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<CurriculumGroup> Groups { get; set; }
        public virtual ICollection<CurriculumBandBlock> BandAssignments { get; set; }
    }
}