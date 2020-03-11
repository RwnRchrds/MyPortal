using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models
{
    [Table("Enrolment")]
    public partial class Enrolment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid BandId { get; set; }

        public virtual CurriculumBand Band { get; set; }

        public virtual Student Student { get; set; }
    }
}
