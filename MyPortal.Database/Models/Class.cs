using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MyPortal.Database.Models
{
    [Table("Class")]
    public class Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            Sessions = new HashSet<Session>();
        }

        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        public Guid AcademicYearId { get; set; }

        [Column(Order = 2)]
        public Guid? SubjectId { get; set; }

        [Column (Order = 3)]
        public Guid BandId { get; set; }

        [Column (Order = 4)]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column (Order = 5)]
        public Guid? YearGroupId { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual YearGroup YearGroup { get; set; }

        public virtual CurriculumBand Band { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
