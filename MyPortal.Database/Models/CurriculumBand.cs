using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid AcademicYearId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public virtual ICollection<Enrolment> Enrolments { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}