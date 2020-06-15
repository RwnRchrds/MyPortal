using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("CurriculumBand")]
    public class CurriculumBand
    {
        public CurriculumBand()
        {
            Enrolments = new HashSet<Enrolment>();
            Classes = new HashSet<Class>();
        }

        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataMember]
        public Guid AcademicYearId { get; set; }

        [DataMember]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [DataMember]
        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<Enrolment> Enrolments { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}