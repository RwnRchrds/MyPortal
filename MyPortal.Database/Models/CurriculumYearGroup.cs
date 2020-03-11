using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("CurriculumYearGroup")]
    public class CurriculumYearGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int KeyStage { get; set; }

        public virtual ICollection<YearGroup> YearGroups { get; set; }
    }
}