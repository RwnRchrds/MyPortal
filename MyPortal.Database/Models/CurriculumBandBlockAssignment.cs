using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Models
{
    [Table("CurriculumBandBlockAssignment")]
    public class CurriculumBandBlockAssignment : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public Guid Id { get; set; }
        
        [Column(Order = 1)]
        public Guid BlockId { get; set; }
        
        [Column(Order = 2)]
        public Guid BandId { get; set; }

        public virtual CurriculumBlock Block { get; set; }
        public virtual CurriculumBand Band { get; set; }
    }
}