using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Enrolment")]
    public class Enrolment
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid BandId { get; set; }

        public virtual CurriculumBand Band { get; set; }

        public virtual Student Student { get; set; }
    }
}
