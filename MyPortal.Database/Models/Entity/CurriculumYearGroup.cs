using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity
{
    [Table("CurriculumYearGroups")]
    public class CurriculumYearGroup : BaseTypes.Entity
    {
        [Column(Order = 1)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 2)]
        public int KeyStage { get; set; }

        [Column(Order = 3)]
        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public virtual ICollection<CurriculumBand> Bands { get; set; }
        public virtual ICollection<YearGroup> YearGroups { get; set; }
    }
}