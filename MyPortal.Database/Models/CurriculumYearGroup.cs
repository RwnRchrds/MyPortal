using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("CurriculumYearGroup")]
    public class CurriculumYearGroup
    {
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column(Order = 2)]
        public int KeyStage { get; set; }

        public virtual ICollection<CurriculumBand> Bands { get; set; }
        public virtual ICollection<YearGroup> YearGroups { get; set; }
    }
}